namespace MachineACafé;

public record RelevésStocksEtConsommations(int CafésServis, int StockGobelets, int ConsommationEau)
{
    public static RelevésStocksEtConsommations operator-(RelevésStocksEtConsommations a, RelevésStocksEtConsommations b)
    {
        return new RelevésStocksEtConsommations(
            a.CafésServis - b.CafésServis, 
            a.StockGobelets - b.StockGobelets,
            a.ConsommationEau - b.ConsommationEau);
    }

    internal RelevésStocksEtConsommations MoinsUnCafé(bool mugPrésent)
        => new (CafésServis + 1, StockGobelets - (mugPrésent ? 0 : 1), ConsommationEau + 1);

    internal bool AuMoinsUnGobelet() => StockGobelets > 0;
}