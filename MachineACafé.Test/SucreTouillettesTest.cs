namespace MachineACafé.Test;

public class SucreTouillettesTest
{
    [Fact(DisplayName = "ETANT DONNE une machine a café ET que du sucre a été demandé " +
                        "QUAND on insère une somme supérieure ou égale à 40 cts " +
                        "ALORS un café est servi " +
                        "ET une dose de sucre est consommée " +
                        "ET une touillette est consommée", Skip = "TODO")]
    public void SucreEtTouilletteTest()
    {
    }

    [Fact(DisplayName = "ETANT DONNE une machine a café n'ayant plus de touillette " +
                        "ET que du sucre a été demandé " +
                        "QUAND on insère une somme supérieure ou égale à 40 cts " +
                        "ALORS un café est servi " +
                        "ET une dose de sucre est consommée ", Skip = "TODO")]
    public void SucreSansTouilletteTest()
    {
    }

    [Fact(DisplayName = "ETANT DONNE une machine a café n'ayant plus de sucre ET que du sucre a été demandé " +
                        "QUAND on insère une somme supérieure ou égale à 40 cts " +
                        "ALORS aucun café n'est servi " +
                        "ET aucune touillette n'est consommée " +
                        "ET la somme est rendue", Skip = "TODO")]
    public void PénurieSucreTest()
    {
    }
}