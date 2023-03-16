namespace MachineACafé.Test.Utiities;

internal static class PrimitivesCartésiennes
{
    private static readonly Random Random = new();

    public static IEnumerable<Ressource> RessourcesNécessaires
        => new[] { Ressource.Café, Ressource.Eau, Ressource.Gobelet };

    public static IEnumerable<int> SommesStrictementInférieuresAuPrixDuCafé
        => new[] { Machine.PrixDuCafé - 1, 1, Random.Next(2, Machine.PrixDuCafé - 2) };
}