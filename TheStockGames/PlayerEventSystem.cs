namespace TheStockGames;

public class PlayerEventSystem
{
    private readonly List<PlayerEvent> _events = new List<PlayerEvent>();
    private readonly Random _rng = new Random();

    public PlayerEventSystem()
    {
        // Money losing events
        _events.Add(new PlayerEvent(
            "Your car breaks down on the way to work. The mechanic charges you $50 just to look at it.",
            -50.00m));
        
        _events.Add(new PlayerEvent(
            "You accidentally order 47 items from Amazon instead of 4. Returns pending. Money gone.",
            -80.00m));
        
        _events.Add(new PlayerEvent(
            "Barry's lawyer sends you a reminder letter. You owe $20 in 'emotional damages' for making him write it.",
            -20.00m));
        
        _events.Add(new PlayerEvent(
            "You go out for 'just one drink'. One drink becomes seven. Uber home costs $45.",
            -45.00m));
        
        _events.Add(new PlayerEvent(
            "Your landlord raises rent. You pay $60 extra this month. He buys a boat.",
            -60.00m));
        
        _events.Add(new PlayerEvent(
            "You get a parking ticket outside the courthouse. The irony is not lost on you.",
            -35.00m));
        
        _events.Add(new PlayerEvent(
            "You impulse buy a course called 'How To Get Rich Quick'. It costs $100. It is a PDF.",
            -100.00m));
        
        _events.Add(new PlayerEvent(
            "Gerald's vet bill arrives. Barry is charging you for it. Somehow legal. You pay $75.",
            -75.00m));
        
        _events.Add(new PlayerEvent(
            "You drop your phone. Screen repair costs $90. The repair guy also scratches it.",
            -90.00m));
        
        _events.Add(new PlayerEvent(
            "You forget to cancel a free trial from 3 years ago. $55 gone. You don't even know what it was.",
            -55.00m));
        
        _events.Add(new PlayerEvent(
            "Your friend asks to borrow $40. You say yes before he finishes the sentence. Classic you.",
            -40.00m));
        
        _events.Add(new PlayerEvent(
            "Speeding ticket. You were doing 34 in a 30. The officer does not care about Gerald.",
            -70.00m));

        // Money gaining events
        _events.Add(new PlayerEvent(
            "You find $20 in an old jacket. It's your money. You feel like you won the lottery.",
            +20.00m));
        
        _events.Add(new PlayerEvent(
            "Your nan sends you a birthday card with $50 inside. It is not your birthday. Don't question it.",
            +50.00m));
    }

    public PlayerEvent? TryFireEvent()
    {
        if (_rng.NextDouble() < 0.35)
            return _events[_rng.Next(_events.Count)];
        return null;
    }

}