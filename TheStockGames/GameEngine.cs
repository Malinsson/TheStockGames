namespace TheStockGames;

public class GameEngine
{
    private readonly Market _market;
    private readonly Player _player;
    private readonly EventSystem _eventSystem;
    private readonly PlayerEventSystem _playerEventSystem;
    private readonly CLIRenderer _renderer;
    private int _turnNumber = 1;
    private readonly decimal _winGoal = 10000.00m;
    
    public GameEngine()
    {
        _market = new Market();
        _eventSystem = new EventSystem();
        _playerEventSystem = new PlayerEventSystem();
        _renderer = new CLIRenderer();

        Console.Write("Enter your name: ");
        var name = Console.ReadLine();
        _player = new Player(name, 300.00m);
    }
    
    public void Run()
    {
        _renderer.RenderIntro(_player.Name);
        
        while (_turnNumber < 30)
        {
            _renderer.RenderMarket(_market, _player, _turnNumber);
        
            StockEvent? ev = _eventSystem.TryFireEvent();
            if (ev != null)
            {
                var firedEvent = ev.Value;
                var affected = _market.Stocks.Find(s => s.Ticker == firedEvent.Ticker);

                if (affected != null)
                {
                    var oldPrice = affected.Price;
                    affected.Price *= (decimal)firedEvent.Modifier;
                    affected.PriceChange = affected.Price - oldPrice;
                }

                _renderer.RenderEvent(firedEvent);
                _renderer.RenderMarket(_market, _player, _turnNumber);
            }
            
            PlayerEvent? playerEv = _playerEventSystem.TryFireEvent();
            if (playerEv != null)
            {
                PlayerEvent firedEvent = playerEv.Value;
                _player.Cash += firedEvent.Amount;
                _renderer.RenderPlayerEvent(firedEvent);
                _renderer.RenderMarket(_market, _player, _turnNumber);
            }
            
            if (IsWinner())
            {
                _renderer.RenderWin(_player, _winGoal);
                break;
            }

            if (IsBankrupt())
            {
                BankruptcyHandler.Handle(_player);
                
                if (_player.Cash <= 0)
                {
                    _renderer.RenderGameOver(_player);
                    break;
                }
            }
            
            if (_player.Debt > 0)
            {
                var interest = _player.Debt * _player.InterestRate;
                _player.Debt += interest;
                _player.Cash -= interest;
                Console.WriteLine($"Interest charged: ${interest:F2}. Total debt: ${_player.Debt:F2}");
            }

            var validCommand = false;
            while (!validCommand)
            {
                Console.Write("\n  > ");
                var command = Console.ReadLine()?.ToLower().Trim();
                validCommand = HandleCommand(command);
            }
            
            _market.TickPrices();
            _turnNumber++;
        }
        
        _renderer.RenderGameOver(_player);
        
    }
    
    private bool HandleCommand(string command)
    {
        switch (command)
        {
            case "buy":
                HandleBuy();
                return true;
            case "sell":
                HandleSell();
                return true;
            case "portfolio":
                _renderer.RenderPortfolio(_player, _market);
                return false;
            case "quit":
                _renderer.RenderGameOver(_player);
                Environment.Exit(0);
                return true;
            case "":
                return true;
            default:
                Console.WriteLine("  Unknown command. Try: buy, sell, portfolio, quit or enter key to progress to the next turn");
                return false;
        }
    }
    
    private void HandleBuy()
    {
        var validBuy = false;

        while (!validBuy)
        {
            Console.Write("Enter ticker: ");
            var ticker = Console.ReadLine()?.ToUpper();

            var stock = _market.Stocks.Find(s => s.Ticker == ticker);
            if (stock == null)
            {
                Console.WriteLine("Stock not found! Try again!");
                return;
            }
            
            Console.Write("How many shares? ");
            if (int.TryParse(Console.ReadLine(), out var quantity))
            {
                
                _player.Buy(stock, quantity);
                validBuy = true;
            }
            else
            {
                var maxQuantity = _player.Cash / (stock.Price * quantity);
                Console.WriteLine($"Invalid quantity! You can only buy {maxQuantity} shares");
            }
        }
    }
        

    private void HandleSell()
    {
        Console.Write("Enter ticker: ");
        var ticker = Console.ReadLine().ToUpper();

        var stock = _market.Stocks.Find(s => s.Ticker == ticker);
        if (stock == null)
        {
            Console.WriteLine("Stock not found!");
            return;
        }

        while (true)
        {
            Console.Write("How many shares? ");
            
            if (int.TryParse(Console.ReadLine(), out var quantity))
            {
                var amountToSell = Math.Min(quantity, _player.Holdings[stock.Ticker] );   
                _player.Sell(stock, amountToSell);
                break;
            }
            
            Console.WriteLine("Invalid quantity! Try again");
        }
    }
    
    private bool IsBankrupt()
    {
        var portfolioValue = _player.CalculateHoldings(_player, _market);
        return (_player.Cash + portfolioValue) < 1.00m;
    }
    
    private bool IsWinner()
    {
        var totalValue = _player.CalculateHoldings(_player, _market);
        return totalValue >= _winGoal;
    }
    
    
}