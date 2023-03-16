namespace MachineACafé.Test.Utiities;

internal class MachineBuilder
{
    public static Machine Default => new MachineBuilder().Build();

    public Machine Build()
    {
        Machine machine = _ressourceManquante switch
               {
                   Ressource.Gobelet => new(stockGobelets: 0),
                   Ressource.Café    => new(caféDisponible: false),
                   Ressource.Eau     => new(eauDisponible: false),
                   null              => new(),
                   _                 => throw new ArgumentOutOfRangeException()
               };

        if (_détectantUnMug) 
            machine.MugDétecté = true;

        return machine;
    }

    private Ressource? _ressourceManquante;
    private bool _détectantUnMug;

    public MachineBuilder AyantUnManque(Ressource ressourceManquante)
    {
        _ressourceManquante = ressourceManquante;
        return this;
    }

    public MachineBuilder DétectantUnMug()
    {
        _détectantUnMug = true;
        return this;
    }
}