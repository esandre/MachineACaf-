namespace MachineACafé.Test.Utiities;

internal class MachineBuilder
{
    public static Machine Default => new MachineBuilder().Build();

    private Ressource? _ressourceManquante;

    public MachineBuilder AyantUnManque(Ressource ressourceManquante)
    {
        _ressourceManquante = ressourceManquante;
        return this;
    }

    public Machine Build()
        => _ressourceManquante switch
           {
               Ressource.Gobelet => new(gobeletsDisponibles: false),
               Ressource.Café    => new(caféDisponible: false),
               Ressource.Eau     => new(eauDisponible: false),
               null              => new(),
               _                 => throw new ArgumentOutOfRangeException()
           };
}