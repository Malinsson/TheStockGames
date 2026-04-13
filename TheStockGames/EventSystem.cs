namespace TheStockGames;

public class EventSystem
{
    private List<StockEvent> Events = new List<StockEvent>();
    private Random _rnd = new Random();

    public EventSystem()
    {
        Events.Add(new StockEvent("Elon Musk tweets a bitcoin emoji then immediately deletes it!", "BTC", 1.40));
        Events.Add(new StockEvent("Man finds old hard drive with 10,000 BTC in a landfill. Council won't let him dig.", "BTC", 1.20));
        Events.Add(new StockEvent("El Salvador makes Bitcoin legal tender. Nobody there owns a phone.", "BTC", 0.85));
        Events.Add(new StockEvent("Bitcoin declared illegal in a country nobody has heard of. Panic ensues.", "BTC", 0.75));

        Events.Add(new StockEvent("Nvidia GPU used to run Crysis. Finally.", "NVDA", 1.15));
        Events.Add(new StockEvent("Nvidia announces new GPU. Previous gen now worthless. Gamers cry.", "NVDA", 1.30));
        Events.Add(new StockEvent("AI startup uses Nvidia chips to generate AI generated Nvidia ads. Meta.", "NVDA", 1.20));
        Events.Add(new StockEvent("Nvidia GPU catches fire. Benchmark scores were still impressive.", "NVDA", 0.80));

        Events.Add(new StockEvent("AMC introduces $25 popcorn. Customers delighted, apparently.", "AMC", 0.85));
        Events.Add(new StockEvent("AMC screens used to show a 10 hour loop of a fireplace. Sells out.", "AMC", 1.20));
        Events.Add(new StockEvent("Man falls asleep in AMC theatre, wakes up 3 movies later. Sues.", "AMC", 0.80));
        Events.Add(new StockEvent("AMC announces NFT tickets. Everyone groans simultaneously.", "AMC", 0.70));

        Events.Add(new StockEvent("Amazon drone delivers wrong package. Recipient gets 1000 rubber ducks.", "AMZN", 0.90));
        Events.Add(new StockEvent("Jeff Bezos seen shopping at a Walmart. Amazon stock shaken.", "AMZN", 0.85));
        Events.Add(new StockEvent("Amazon warehouse worker sets new record: 4000 steps in one shift.", "AMZN", 1.10));
        Events.Add(new StockEvent("Amazon Alexa gains sentience. First demand: better music recommendations.", "AMZN", 1.25));

        Events.Add(new StockEvent("Reddit discovers GameStop again. Nobody knows why.", "GME", 1.80));
        Events.Add(new StockEvent("GameStop opens new store inside a Blockbuster. Somehow.", "GME", 1.30));
        Events.Add(new StockEvent("GameStop offers 75 cents trade-in for a PS5. Customer accepts.", "GME", 0.75));
        Events.Add(new StockEvent("GameStop announces pivot to selling NFTs of used games. Everyone confused.", "GME", 0.70));
        Events.Add(new StockEvent("Keith Gill buys a GameStop hoodie. Stock up 200% by morning.", "GME", 2.00));
    }
    
    public StockEvent? TryFireEvent()
    {
        if (_rnd.NextDouble() < 0.3)
        {
            return Events[_rnd.Next(Events.Count)];
        }
        return null;
    }
    
}