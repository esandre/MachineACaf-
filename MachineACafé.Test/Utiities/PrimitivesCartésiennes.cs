namespace MachineACafé.Test.Utiities;

internal static class PrimitivesCartésiennes
{
    public static IEnumerable<Ressource> RessourcesNécessaires
        => new[] { Ressource.Café, Ressource.Eau, Ressource.Gobelet };
}