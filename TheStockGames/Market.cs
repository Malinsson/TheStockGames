namespace TheStockGames;

public class Market
{
    public readonly List<Stock> Stocks = new List<Stock>();

    public Market()
    {
        Stocks.Add(new Stock("Bitcoin", "BTC", 400.00m){ Volatility = 0.8 });
        Stocks.Add(new Stock("Nvidia", "NVDA", 170.00m) { Volatility = 0.4 });
        Stocks.Add(new Stock("AMC Entertainment", "AMC", 30.00m) { Volatility = 0.4 });
        Stocks.Add(new Stock("Amazon", "AMZN", 200.00m));
        Stocks.Add(new Stock("GameStop", "GME", 60.00m){ Volatility = 1 });
    }

    public void TickPrices()
        {

            foreach (var stock in Stocks)
            {
                stock.UpdatePrice();
            }
            
        }

}
