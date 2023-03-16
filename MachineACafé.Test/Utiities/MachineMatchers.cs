using NFluent;
using NFluent.Extensibility;
using NFluent.Kernel;
// ReSharper disable UnusedMethodReturnValue.Global

namespace MachineACafé.Test.Utiities;

internal static class MachineMatchers
{
    public static ICheckLink<ICheck<Machine>> AucuneRessourceNécessaireConsommée(
        this ICheck<Machine> check, 
        RelevésStocksEtConsommations initial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);

        return checker.BuildChainingObject()
            .And.AucunCaféServi(initial)
            .And.AucunGobeletConsommé(initial)
            .And.PasDEauConsommée(initial);
    }

    public static ICheckLink<ICheck<Machine>> ConsommeUnDeChaque(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial,
        params Ressource[] ressources)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);

        return ressources.Aggregate(checker.BuildChainingObject(),
            (current, ressource) => current.And.ConsommeUn(étatInitial, ressource)
        );
    }

    public static ICheckLink<ICheck<Machine>> ConsommeUn(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial,
        Ressource ressource)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);

        switch (ressource)
        {
            case Ressource.Gobelet:
                return checker.BuildChainingObject().And.UnGobeletConsommé(étatInitial);
            case Ressource.Café:
                return checker.BuildChainingObject().And.UnCaféServi(étatInitial);
            case Ressource.Eau:
                return checker.BuildChainingObject().And.UnVolumeDEauConsommé(étatInitial);
            case Ressource.Contenant:
                throw new NotSupportedException();
            default:
                throw new ArgumentOutOfRangeException(nameof(ressource), ressource, null);
        }
    }

    public static ICheckLink<ICheck<Machine>> UnCaféServi(
        this ICheck<Machine> check, 
        RelevésStocksEtConsommations relevéInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - relevéInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.CafésServis != 1)
                    throw new FluentCheckException(
                        $"Un café devait être servi. {différentiel.CafésServis} cafés servis."
                    );
            },
            $"{différentiel.CafésServis} cafés ont été servis alors qu'un seul était attendu.");
    }

    public static ICheckLink<ICheck<Machine>> AucunCaféServi(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - étatInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.CafésServis != 0)
                    throw new FluentCheckException(
                        $"Aucun café ne devait être servi. {différentiel.CafésServis} cafés servis."
                    );
            }, 
            $"{différentiel.CafésServis} cafés ont été servis alors que cela ne devrait pas être le cas.");
    }

    public static ICheckLink<ICheck<Machine>> UnGobeletConsommé(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - étatInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.StockGobelets != -1)
                    throw new FluentCheckException(
                        $"Un gobelet ne devait être utilisé. {différentiel.StockGobelets} gobelets consommés."
                    );
            },
            $"{différentiel.StockGobelets} gobelets ont été consommés alors qu'un seul devait l'être.");
    }

    private static ICheckLink<ICheck<Machine>> AucunGobeletConsommé(
        this ICheck<Machine> check, 
        RelevésStocksEtConsommations étatInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - étatInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.StockGobelets != 0)
                    throw new FluentCheckException(
                        $"Aucun gobelet ne devait être utilisé. {différentiel.StockGobelets} gobelets consommés."
                    );
            },
            $"{différentiel.StockGobelets} gobelets ont été consommés alors que cela ne devrait pas être le cas.");
    }

    public static ICheckLink<ICheck<Machine>> UnVolumeDEauConsommé(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - étatInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.ConsommationEau != 1)
                    throw new FluentCheckException(
                        "Un volume d'eau ne devait être consommé. " +
                        $"{différentiel.ConsommationEau} volumes consommés."
                    );
            },
            $"{différentiel.ConsommationEau} volumes d'eau ont été consommés alors qu'un seul devait l'être.");
    }

    private static ICheckLink<ICheck<Machine>> PasDEauConsommée(
        this ICheck<Machine> check,
        RelevésStocksEtConsommations étatInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        var etatFinal = checker.Value.ObtenirRelevéStocksConsommations();
        var différentiel = etatFinal - étatInitial;

        return checker.ExecuteCheck(() =>
            {
                if (différentiel.ConsommationEau != 0)
                    throw new FluentCheckException(
                        "Aucun volume d'eau ne devait être consommé. " +
                        $"{différentiel.ConsommationEau} volumes consommés."
                    );
            },
            $"{différentiel.ConsommationEau} volumes d'eau ont été consommés alors que cela ne devrait pas être le cas.");
    }

    public static ICheckLink<ICheck<Machine>> SommeEncaissée(this ICheck<Machine> check, int argentInitial, int sommeAttendue)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        return checker.ExecuteCheck(() =>
            {
                if (checker.Value.SommeEnCaisse != argentInitial + sommeAttendue)
                    throw new FluentCheckException(
                        $"{sommeAttendue} devrait être encaissé. {checker.Value.SommeEnCaisse - argentInitial} encaissé."
                    );
            },
            $"{checker.Value.SommeEnCaisse - argentInitial} centimes ont été encaissés alors que {sommeAttendue} était attendu.");
    }

    public static ICheckLink<ICheck<Machine>> PrixDUnCaféEncaissé(this ICheck<Machine> check, int argentInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        return checker.ExecuteCheck(() =>
            {
                if (checker.Value.SommeEnCaisse != argentInitial + Machine.PrixDuCafé)
                    throw new FluentCheckException(
                        $"Le prix d'un café devrait être encaissé. {checker.Value.SommeEnCaisse - argentInitial} encaissé."
                    );
            },
            $"{checker.Value.SommeEnCaisse - argentInitial} centimes ont été encaissés alors que le prix d'un café était attendu.");
    }

    public static ICheckLink<ICheck<Machine>> ArgentRendu(this ICheck<Machine> check, int argentInitial)
    {
        var checker = ExtensibilityHelper.ExtractChecker(check);
        return checker.ExecuteCheck(() =>
            {
                if (checker.Value.SommeEnCaisse != argentInitial)
                    throw new FluentCheckException(
                        $"L'argent devrait être restitué. {checker.Value.SommeEnCaisse - argentInitial} encaissé."
                    );
            },
            $"{checker.Value.SommeEnCaisse - argentInitial} centimes ont été encaissés alors que cela ne devrait pas être le cas.");
    }
}