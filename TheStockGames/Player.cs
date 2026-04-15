namespace TheStockGames;

public class Player(string name, decimal startingCash)
{
    public readonly string Name = name;
    public readonly decimal StartingCash = startingCash;
    public decimal Cash = startingCash;
    public decimal Debt = 0.00m;
    public decimal InterestRate = 0.00m;
    public readonly Dictionary<string, int> Holdings = new Dictionary<string, int>();
    

    public bool Buy(Stock stock, int amount)
    {
        var totalCost = stock.Price * amount;
        
        if (totalCost > Cash)
        {
            return false;
        }
        
        Cash -= totalCost;

        if (Holdings.ContainsKey(stock.Ticker))
            Holdings[stock.Ticker] += amount;
        else
            Holdings[stock.Ticker] = amount;

        return true;
    }

    public bool Sell(Stock stock, int amount)
    {
        if (!Holdings.ContainsKey(stock.Ticker))
        {
            return false;
        }

        if (Holdings[stock.Ticker] < amount)
        {
            return false;
        }

        Cash += amount * stock.Price;
        Holdings[stock.Ticker] -= amount;
        
        if (Holdings[stock.Ticker] == 0)
            Holdings.Remove(stock.Ticker);

        return true;
    }

    public decimal CalculateHoldings(Player player, Market market)
    {
        decimal portfolioValue = 0;
        foreach (var holding in player.Holdings)
        {
            Stock stock = market.Stocks.Find(s => s.Ticker == holding.Key);
            portfolioValue += stock.Price * holding.Value;
        }

        return portfolioValue;
    }
}