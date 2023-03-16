namespace MachineACafé;

public class Machine
{
    private readonly bool _eauDisponible;
    private readonly bool _caféDisponible;

    private RelevésStocksEtConsommations _étatActuel;

    public const int PrixDuCafé = 40;

    public int SommeEnCaisse { get; private set; }
    public bool MugDétecté { private get; set; }

    public Machine(
        bool eauDisponible = true, 
        int stockGobelets = 1,
        bool caféDisponible = true)
    {
        _eauDisponible = eauDisponible;
        _caféDisponible = caféDisponible;

        _étatActuel = new RelevésStocksEtConsommations(0, stockGobelets, 0);
    }

    private bool PossèdeUnContenant => MugDétecté || _étatActuel.AuMoinsUnGobelet();
    private bool PeutTechniquementServirUnCafé => PossèdeUnContenant && _caféDisponible && _eauDisponible;
    private bool PeutServirUnCafé(int sommeInsérée) => PeutTechniquementServirUnCafé && sommeInsérée >= PrixDuCafé;

    public void Insérer(int sommeEnCentimes)
    {
        if (!PeutServirUnCafé(sommeEnCentimes)) return;

        SommeEnCaisse += sommeEnCentimes;
        _étatActuel = _étatActuel.MoinsUnCafé(MugDétecté);
    }

    public RelevésStocksEtConsommations ObtenirRelevéStocksConsommations() => _étatActuel;
}