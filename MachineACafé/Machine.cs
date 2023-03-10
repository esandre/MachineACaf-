namespace MachineACafé;

public class Machine
{
    private readonly bool _eauDisponible;
    private readonly bool _caféDisponible;

    public const int PrixDuCafé = 40;

    public int CafésServis { get; private set; }
    public int SommeEnCaisse { get; private set; }
    public bool MugDétecté { private get; set; }
    public int StockGobelets { get; }

    public Machine(
        bool eauDisponible = true, 
        int stockGobelets = 1,
        bool caféDisponible = true)
    {
        _eauDisponible = eauDisponible;
        _caféDisponible = caféDisponible;
        StockGobelets = stockGobelets;
    }

    public void Insérer(int sommeEnCentimes)
    {
        if(!MugDétecté)
        {
            if (!_eauDisponible) return;
            if (StockGobelets == 0) return;
            if (!_caféDisponible) return;
            if (sommeEnCentimes < PrixDuCafé) return;
        }

        CafésServis++;
        SommeEnCaisse += sommeEnCentimes;
    }
}