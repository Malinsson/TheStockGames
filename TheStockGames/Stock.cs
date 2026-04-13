namespace TheStockGames;

public class Stock(string name, string ticker, decimal startPrice)
{
    public readonly string Name = name;
    public readonly string Ticker = ticker;
    public readonly decimal StartingPrice = startPrice;
    public decimal Price = startPrice;
    public decimal PriceChange = 0;
    public double Volatility = 0.2;

    private readonly Random _rnd = new Random();
    
    public void UpdatePrice()
    {
        var change = (_rnd.NextDouble() * Volatility) - (Volatility / 2);
        var newPrice = Price * (decimal)(1 + change);
        PriceChange = newPrice - Price;
        Price = newPrice;

    }
}