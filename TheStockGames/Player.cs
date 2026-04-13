namespace TheStockGames;

public class Player(string name, decimal startingCash)
{
    public readonly string Name = name;
    public readonly decimal StartingCash = startingCash;
    public decimal Cash = startingCash;
    public decimal Debt = 0.00m;
    public decimal InterestRate = 0.00m;
    public readonly Dictionary<string, int> Holdings = new Dictionary<string, int>();
    

    public void Buy(Stock stock, int amount)
    {
        var totalCost = stock.Price * amount;
        
        if (totalCost > Cash)
        {
            Console.WriteLine("You don't have enough money to buy that!");
            return;
        }
        
        Cash -= totalCost;

        if (Holdings.ContainsKey(stock.Ticker))
            Holdings[stock.Ticker] += amount;
        else
            Holdings[stock.Ticker] = amount;
        
    }

    public void Sell(Stock stock, int amount)
    {
        if (!Holdings.ContainsKey(stock.Ticker))
        {
            Console.WriteLine("You don't own any stocks there!");
            return;
        }

        if (Holdings[stock.Ticker] < amount)
        {
            Console.WriteLine("You don't have enough shares!");
            return;
        }

        Cash += amount * stock.Price;
        Holdings[stock.Ticker] -= amount;
        
        if (Holdings[stock.Ticker] == 0)
            Holdings.Remove(stock.Ticker);
        
    }
}