namespace TheStockGames;

public class CLIRenderer
{

    public void RenderMarket(Market market, Player player, int turnNumber)
    {
        Console.Clear();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        BIG STONKS SIMULATOR            ║");
        Console.WriteLine($"║             Turn {turnNumber,-22}║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();

        var totalValue = 0.00m;

        foreach (var holding in  player.Holdings)
        {
            Stock stock = market.Stocks.Find(s => s.Ticker == holding.Key);
            var value = stock.Price * holding.Value;
            totalValue += value;
        }
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  Player    : {player.Name}");
        Console.WriteLine($"  Cash      : ${player.Cash:F2}");
        Console.WriteLine($"  Holdings  : ${totalValue:F2}");
        if (player.Debt > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Debt   : ${player.Debt:F2} ({player.InterestRate * 100}% per turn)");
        }
        Console.ResetColor();

        // Market table
        Console.WriteLine("\n  ┌───────────────────────────────────────────────┐");
        Console.WriteLine("  │  MARKET                                       │");
        Console.WriteLine("  ├──────┬────────────────────┬──────────┬────────┤");
        Console.WriteLine("  │ TKR  │ Name               │ Price    │ Change │");
        Console.WriteLine("  ├──────┼────────────────────┼──────────┼────────┤");

        foreach (Stock stock in market.Stocks)
        {
            string changeSymbol = stock.PriceChange >= 0 ? "▲" : "▼";
            Console.ForegroundColor = stock.PriceChange >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"  │ {stock.Ticker,-4} │ {stock.Name,-18} │ ${stock.Price,-8:F2}│ {changeSymbol}{Math.Abs(stock.PriceChange),-6:F2} │");
            Console.ResetColor();
        }

        Console.WriteLine("  └──────┴────────────────────┴──────────┴────────┘");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n  Commands: buy | sell | portfolio | quit | enter key to progress to the next turn");
        Console.ResetColor();
        
    }

    public void RenderPortfolio(Player player, Market market)
    {
        Console.WriteLine("\n  ┌─────────────────────────────────────────────────┐");
        Console.WriteLine("  │  PORTFOLIO                                      │");
        Console.WriteLine("  ├──────┬────────┬──────────┬──────────┬───────────┤");
        Console.WriteLine("  │ TKR  │ Shares │ Price    │ Value    │ Profit    │");
        Console.WriteLine("  ├──────┼────────┼──────────┼──────────┼───────────┤");

        var totalValue = player.Cash;

        if (player.Holdings.Count == 0)
        {
            Console.WriteLine("  │ You own no stocks!                              │");
        }
        else
        {
            foreach (var holding in player.Holdings)
            {
                Stock stock = market.Stocks.Find(s => s.Ticker == holding.Key);
                var value = stock.Price * holding.Value;
                var profit = value - (stock.StartingPrice * holding.Value);
                totalValue += value;

                Console.ForegroundColor = profit >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"  │ {stock.Ticker,-4} │ {holding.Value,-6} │ ${stock.Price,-8:F2}│ ${value,-8:F2}│ ${profit,-9:F2}│");
                Console.ResetColor();
            }
        }

        Console.WriteLine("  ├──────┴────────┴──────────┴──────────┴───────────┤");

        decimal netProfit = totalValue - player.StartingCash;
        Console.ForegroundColor = netProfit >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"  │ Cash:         ${player.Cash,-9:F2}|");
        Console.WriteLine($"  │ Total Value:  ${totalValue,-9:F2}|");
        Console.WriteLine($"  │ Net Profit:   ${netProfit,-9:F2}|");
        Console.ResetColor();
        Console.WriteLine("  └─────────────────────────────────────────────────┘");

        Console.Write("\n  Press any key to continue...");
        Console.ReadKey();
    }

    public void RenderEvent(StockEvent ev)
    {
        var width = 44;
        string[] words = ev.Message.Split(' ');
        List<string> lines = new List<string>();
        var current = "";

        foreach (var word in words)
        {
            if ((current + " " + word).Trim().Length > width)
            {
                lines.Add(current.Trim());
                current = word;
            }
            else
            {
                current += " " + word;
            }
        }
        lines.Add(current.Trim());

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("  ╔══════════════════════════════════════════════╗");
        Console.WriteLine("  ║  BREAKING NEWS                               ║");
        Console.WriteLine("  ╠══════════════════════════════════════════════╣");
        foreach (var line in lines)
        {
            Console.WriteLine($"  ║  {line,-44}║");
        }
        Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.Write("\n  Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void RenderGameOver(Player player)
    {
        Console.Clear();
        decimal netProfit = player.Cash - player.StartingCash;

        Console.ForegroundColor = netProfit >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║             GAME OVER                  ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine($"\n  Name          : {player.Name}");
        Console.WriteLine($"  Starting Cash : ${player.StartingCash:F2}");
        Console.WriteLine($"  Final Cash    : ${player.Cash:F2}");

        Console.ForegroundColor = netProfit >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"  Net Profit    : ${netProfit:F2}");
        Console.ResetColor();

        if (netProfit >= 500)
            Console.WriteLine("\n  Warren Buffet has requested your number.");
        else if (netProfit >= 0)
            Console.WriteLine("\n  Not bad. Barry is mildly impressed.");
        else if (netProfit >= -500)
            Console.WriteLine("\n  Your parents are disappointed but not surprised.");
        else
            Console.WriteLine("\n  Barry says hi. You should probably leave town.");
    }
    
    public void RenderWin(Player player, decimal _winGoal)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║             YOU WIN!                   ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine($"\n  {player.Name} you reached ${_winGoal}!");
        Console.WriteLine($"  Final value : ${player.Cash:F2}");
        Console.WriteLine("\n  Barry is furious. Your parents are shocked.");
        Console.WriteLine("  Grandma knits you a celebratory sweater.");
    }
    
    public void RenderIntro(string name)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║           BIG STONKS SIMULATOR 3000                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine($"\n  Welcome, {name}.\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  Here's your situation:");
        Console.ResetColor();

        Console.WriteLine(@"
  Last Tuesday you accidentally reversed your car into a statue 
  of Barry Henderson, beloved local businessman and surprisingly 
  sensitive man.

  Barry is suing you for $10,000.

  Not for the statue. He has plenty of statues. He's suing you 
  because the crash ""emotionally disturbed"" his pet tortoise, Gerald.
  Gerald hasn't eaten since. Barry's lawyer says Gerald ""used to 
  love lettuce"" and now ""just stares."" 

  The court date is in 30 days.

  Your savings account contains $300. Your regular account contains 
  an expired Subway card and a button.

  Your lawyer, who you found on a lamppost, has one piece of advice:

  ""Have you considered the stock market?""

  You have not. But you're considering it now.
    ");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  Goal    : Make $10,000 before Barry takes everything you own.");
        Console.WriteLine("  Capital : $300");
        Console.WriteLine("  Time    : Good luck.");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n  Press any key to begin trading...");
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();
    }
    
    public void RenderPlayerEvent(PlayerEvent ev)
    {
        Console.Clear();
        bool isPositive = ev.Amount >= 0;

        Console.ForegroundColor = isPositive ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine("  ╔══════════════════════════════════════════════╗");
        Console.WriteLine($"  ║  {(isPositive ? "GOOD NEWS" : "BAD NEWS"),-44}║");
        Console.WriteLine("  ╠══════════════════════════════════════════════╣");

        // word wrap the message
        int width = 44;
        string[] words = ev.Message.Split(' ');
        List<string> lines = new List<string>();
        string current = "";
        foreach (string word in words)
        {
            if ((current + " " + word).Trim().Length > width)
            {
                lines.Add(current.Trim());
                current = word;
            }
            else
            {
                current += " " + word;
            }
        }
        lines.Add(current.Trim());

        foreach (string line in lines)
            Console.WriteLine($"  ║  {line,-44}║");

        Console.WriteLine("  ╠══════════════════════════════════════════════╣");
        Console.WriteLine($"  ║  {(isPositive ? "+" : "")}{ev.Amount:F2} added to your cash{"",-26}║");
        Console.WriteLine("  ╚══════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.Write("\n  Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
