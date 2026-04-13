namespace TheStockGames;

public struct StockEvent(string message, string ticker, double modifier)
{
       public readonly string Message = message;
       public readonly string Ticker = ticker;
       public readonly double Modifier = modifier;
    
}