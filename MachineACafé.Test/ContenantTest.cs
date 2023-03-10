namespace MachineACafé.Test;

public class ContenantTest
{
    [Fact(DisplayName = "ETANT DONNE une machine a café détectant un mug" +
                        "QUAND on met une somme supérieure ou égale à 40cts" +
                        "ALORS aucun gobelet n'est consommé", Skip = "TODO")]
    public void MugTest()
    {
    }

    [Fact(DisplayName = "ETANT DONNE une machine a café n'ayant plus de gobelets " +
                        "ET détectant un mug " +
                        "QUAND on met une somme supérieure ou égale à 40cts" +
                        "ALORS un café est servi" +
                        "ET la somme est encaissée", Skip = "TODO")]
    public void MugPénurieGobeletsTest()
    {
    }
}