using System.Reflection.PortableExecutable;
using MachineACafé.Test.Utiities;

namespace MachineACafé.Test;

public class ContenantTest
{
    [Fact(DisplayName = "ETANT DONNE une machine a café détectant un mug" +
                        "QUAND on met une somme supérieure ou égale à 40cts" +
                        "ALORS aucun gobelet n'est consommé", Skip = "Test tautologique")]
    public void MugTest()
    {
        // ETANT DONNE une machine à café détectant un mug
        var machine = new MachineBuilder()
            .DétectantUnMug()
            .Build();

        var gobeletsInitiaux = machine.StockGobelets;

        // QUAND on met une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        const int sommeInsérée = Machine.PrixDuCafé;
        machine.Insérer(sommeInsérée);

        // ALORS aucun gobelet n'est consommé
        Assert.Equal(gobeletsInitiaux, machine.StockGobelets);
    }

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

        var cafésServisInitiaux = machine.CafésServis;
        var sommeEnCaisseInitiale = machine.SommeEnCaisse;
        var gobeletsInitiaux = machine.StockGobelets;

        // QUAND on met une somme supérieure ou égale à 40cts
        // (40cts est une vérification jugée suffisante pour ce test)
        const int sommeInsérée = Machine.PrixDuCafé;
        machine.Insérer(sommeInsérée);

        // ALORS un café est servi
        Assert.Equal(cafésServisInitiaux + 1, machine.CafésServis);

        // ET la somme est encaissée
        Assert.Equal(sommeEnCaisseInitiale + sommeInsérée, machine.SommeEnCaisse);
    }
}