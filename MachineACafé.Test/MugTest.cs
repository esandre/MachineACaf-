using System.Reflection.PortableExecutable;
using MachineACafé.Test.Utiities;
using NFluent;

namespace MachineACafé.Test;

public class MugTest
{
    [Fact(DisplayName = "ETANT DONNE une machine a café n'ayant plus de gobelets " +
                        "ET détectant un mug " +
                        "QUAND on met une somme supérieure ou égale à 40cts" +
                        "ALORS un café est servi" +
                        "ET la somme est encaissée")]
    public void MugPénurieGobeletsTest()
    {
        // ETANT DONNE une machine à café n'ayant plus de gobelets
        // ET détectant un mug
        var machine = new MachineBuilder()
            .DétectantUnMug()
            .AyantUnManque(Ressource.Gobelet)
            .Build();

        var etatInitial = machine.ObtenirRelevéStocksConsommations();
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;

        // QUAND on met une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        const int sommeInsérée = Machine.PrixDuCafé;
        machine.Insérer(sommeInsérée);

        // ALORS un café est servi
        // ET la somme est encaissée
        Check.That(machine).UnCaféServi(etatInitial)
            .And.PrixDUnCaféEncaissé(sommeEnCaisseInitiale);
    }

    public static IEnumerable<object[]> CasPasDeMugMagiqueTest 
        => new CartesianData(PrimitivesCartésiennes.RessourcesNécessaires.Except(new []{ Ressource.Gobelet }));

    [Theory(DisplayName = "ETANT DONNE une machine a café n'ayant plus d'une Ressource Nécessaire sauf les gobelets " +
                          "ET détectant un mug " +
                          "QUAND on met une somme supérieure ou égale à 40cts" +
                          "ALORS aucun café n'est servi" +
                          "ET la somme est restituée")]
    [MemberData(nameof(CasPasDeMugMagiqueTest))]
    public void PasDeMugMagiqueTest(Ressource ressourceManquante)
    {
        // ETANT DONNE une machine a café n'ayant plus d'une Ressource Nécessaire sauf les gobelets
        // ET détectant un mug
        var machine = new MachineBuilder()
            .DétectantUnMug()
            .AyantUnManque(ressourceManquante)
            .Build();

        var etatInitial = machine.ObtenirRelevéStocksConsommations();
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;

        // QUAND on met une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        const int sommeInsérée = Machine.PrixDuCafé;
        machine.Insérer(sommeInsérée);

        // ALORS aucun café n'est servi
        // ET la somme est restituée
        Check.That(machine).AucunCaféServi(etatInitial)
            .And.ArgentRendu(sommeEnCaisseInitiale);
    }
}