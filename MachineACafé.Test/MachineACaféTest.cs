using MachineACafé.Test.Utiities;
using NFluent;

namespace MachineACafé.Test;

public class MachineACaféTest
{
    private static readonly Random Random = new ();

    private static IEnumerable<int> SommesSupérieuresOuEgalesAuPrixDuCafé 
        => new[] { Machine.PrixDuCafé, Machine.PrixDuCafé + 1, Random.Next(Machine.PrixDuCafé + 2, int.MaxValue) };

    private static IEnumerable<int> SommesStrictementInférieuresAuPrixDuCafé
        => new[] { Machine.PrixDuCafé - 1, 1, Random.Next(2, Machine.PrixDuCafé - 2) };

    public static IEnumerable<object[]> CasTestPasAssezArgent
        => SommesStrictementInférieuresAuPrixDuCafé
            .Select(somme => new object[] { somme });

    [Theory(DisplayName =
        "ETANT DONNE une machine a café " +
        "QUAND on insère une somme strictement inférieure à 40cts " +
        "ALORS aucun café n'est servi " +
        "ET la somme est rendue")]
    [MemberData(nameof(CasTestPasAssezArgent))]
    public void TestPasAssezArgent(int sommeEnCentimes)
    {
        // ETANT DONNE une machine a café
        var machine = MachineBuilder.Default;
        var étatInitial = machine.ObtenirRelevéStocksConsommations();
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;

        // QUAND on insère une somme strictement inférieure à 40cts
        machine.Insérer(sommeEnCentimes);
        
        // ALORS aucun café n'est servi
        // ET la somme est rendue
        Check.That(machine)
            .AucunCaféServi(étatInitial)
            .And.ArgentRendu(sommeEnCaisseInitiale);
    }

    public static IEnumerable<object[]> CasTestPénurie
        => new CartesianData(PrimitivesCartésiennes.RessourcesNécessaires);

    [Theory(DisplayName =
        "ETANT DONNE une machine a café manquant d'une Ressource Nécessaire " +
        "QUAND on insère une somme supérieure ou égale à 40cts " +
        "ALORS aucune Ressource Nécessaire n'est consommée " +
        "ET la somme est rendue")]
    [MemberData(nameof(CasTestPénurie))]
    public void TestPénuries(Ressource ressourceNécessaireManquante)
    {
        // ETANT DONNE une machine a café manquant d'une Ressource Nécessaire
        var machine = new MachineBuilder()
            .AyantUnManque(ressourceNécessaireManquante)
            .Build();

        var relevésStocksConsommationsInitial = machine.ObtenirRelevéStocksConsommations();
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;

        // QUAND on insère une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        const int sommeInsérée = Machine.PrixDuCafé;
        machine.Insérer(sommeInsérée);

        // ALORS aucune Ressource Nécessaire n'est consommée
        // ET la somme est rendue
        Check.That(machine)
            .AucuneRessourceNécessaireConsommée(relevésStocksConsommationsInitial)
            .And.ArgentRendu(sommeEnCaisseInitiale);
    }

    public static IEnumerable<object[]> CasTestServirCafé
        => SommesSupérieuresOuEgalesAuPrixDuCafé
            .Select(somme => new object[] { somme });

    [Theory(DisplayName =
        "ETANT DONNE une machine a café" +
        "QUAND on insère une somme supérieure ou égale à 40cts " +
        "ALORS une unité de chaque Ressource Nécessaire est consommée " +
        "ET la somme est encaissée")]
    [MemberData(nameof(CasTestServirCafé))]
    public void TestServirCafé(int sommeEnCentimes)
    {
        // ETANT DONNE une machine a café
        var machine = MachineBuilder.Default;

        var etatInitial = machine.ObtenirRelevéStocksConsommations();
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;

        // QUAND on insère une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        machine.Insérer(sommeEnCentimes);

        // ALORS une unité de chaque Ressource Nécessaire est consommée
        // ET la somme est encaissée
        Check.That(machine)
            .ConsommeUnDeChaque(etatInitial, PrimitivesCartésiennes.RessourcesNécessaires.ToArray())
            .And.SommeEncaissée(sommeEnCaisseInitiale, sommeEnCentimes);
    }
}