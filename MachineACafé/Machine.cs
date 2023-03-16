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

    public void Insérer(int sommeEnCentimes)
    {
        if(!MugDétecté)
        {
            if (!_eauDisponible) return;
            if (!_étatActuel.AuMoinsUnGobelet()) return;
            if (!_caféDisponible) return;
            if (sommeEnCentimes < PrixDuCafé) return;
        }
        
        SommeEnCaisse += sommeEnCentimes;
        _étatActuel = _étatActuel.MoinsUnCafé(MugDétecté);
    }

    public RelevésStocksEtConsommations ObtenirRelevéStocksConsommations() => _étatActuel;
}