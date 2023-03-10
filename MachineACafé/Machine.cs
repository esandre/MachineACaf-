namespace MachineACafé;

public class Machine
{
    private readonly bool _eauDisponible;
    private readonly bool _gobeletsDisponibles;
    private readonly bool _caféDisponible;

    public const int PrixDuCafé = 40;

    public int CafésServis { get; private set; }
    public int SommeEnCaisse { get; private set; }

    public Machine(
        bool eauDisponible = true, 
        bool gobeletsDisponibles = true,
        bool caféDisponible = true)
    {
        _eauDisponible = eauDisponible;
        _gobeletsDisponibles = gobeletsDisponibles;
        _caféDisponible = caféDisponible;
    }

    public void Insérer(int sommeEnCentimes)
    {
        if(!_eauDisponible) return;
        if(!_gobeletsDisponibles) return;
        if(!_caféDisponible) return;
        if (sommeEnCentimes < PrixDuCafé) return;

        CafésServis++;
        SommeEnCaisse += sommeEnCentimes;
    }
}