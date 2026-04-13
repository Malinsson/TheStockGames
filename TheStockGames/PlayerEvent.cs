namespace TheStockGames;

public struct PlayerEvent(string message, decimal amount)
{

    public readonly string Message = message;
    public readonly decimal Amount = amount;
    
}