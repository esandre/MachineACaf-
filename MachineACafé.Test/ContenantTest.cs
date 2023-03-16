using System.Reflection.PortableExecutable;
using MachineACafé.Test.Utiities;
using NFluent;

namespace MachineACafé.Test;

public class ContenantTest
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
}