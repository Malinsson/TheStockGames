namespace TheStockGames;

public class BankruptcyHandler
{
    
    private static int timesGoneBroke = 0;

    public static void Handle(Player player)
    {
        timesGoneBroke++;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n💀 YOU ARE BROKE 💀");
        Console.ResetColor();

        switch (timesGoneBroke)
        {
            case 1:
                CallParents(player);
                break;
            case 2:
                CallGrandparents(player);
                break;
            case 3:
                BankLoan(player);
                break;
            case 4:
                LoanShark(player);
                break;
            default:
                NoOneLeft(player);
                break;
        }
    }

    private static void CallParents(Player player)
    {
        Console.WriteLine("\nYou call your parents...");
        Console.WriteLine("Mum: \"Again?! This is the LAST time we're helping you.\"");
        Console.WriteLine("Dad: \"I told you the stock market was gambling. I TOLD you.\"");
        Console.WriteLine("Mum: \"Fine. We'll lend you $500. But you're coming to dinner on Sunday.\"");
        Console.Write("\nAccept $500 from parents? (yes/no): ");

        if (Console.ReadLine().ToLower() == "yes")
        {
            player.Cash += 500.00m;
            player.Debt += 500.00m;
            Console.WriteLine("Your mum sends you $500 via PayPal with the note: \"Please be careful honey 😢\"");
        }
        else
        {
            Console.WriteLine("You hang up. Bold choice.");
        }
    }

    private static void CallGrandparents(Player player)
    {
        Console.WriteLine("\nYour parents aren't picking up. You call your grandparents...");
        Console.WriteLine("Grandma: \"Oh sweetheart, of course! Money is just paper anyway.\"");
        Console.WriteLine("Grandpa: \"What's a stock? Is this like the lottery?\"");
        Console.WriteLine("Grandma: \"We can lend you $300 from the biscuit tin.\"");
        Console.Write("\nAccept $300 from grandparents? (yes/no): ");

        if (Console.ReadLine().ToLower() == "yes")
        {
            player.Cash += 300.00m;
            player.Debt += 300.00m;
            Console.WriteLine("Grandma mails you a cheque. It arrives 2 weeks later with a $5 note and a butterscotch candy.");
        }
        else
        {
            Console.WriteLine("You tell grandma you're fine. She doesn't believe you.");
        }
    }

    private static void BankLoan(Player player)
    {
        Console.WriteLine("\nYour family has disowned you financially. You visit the bank...");
        Console.WriteLine("Bank Manager: \"Your credit score is... concerning.\"");
        Console.WriteLine("Bank Manager: \"We can offer you $1000 at 20% interest. Per turn.\"");
        Console.WriteLine("Bank Manager: \"Sign here, here, here, initial here, and here.\"");
        Console.Write("\nAccept $1000 bank loan at 20% interest per turn? (yes/no): ");

        if (Console.ReadLine().ToLower() == "yes")
        {
            player.Cash += 1000.00m;
            player.Debt += 1000.00m;
            player.InterestRate = 0.20m;
            Console.WriteLine("You sign. The bank manager does not look optimistic.");
        }
        else
        {
            Console.WriteLine("You walk out. The bank manager looks relieved.");
        }
    }

    private static void LoanShark(Player player)
    {
        Console.WriteLine("\nThe bank won't return your calls. A man named Barry finds you...");
        Console.WriteLine("Barry: \"I heard you need cash. I can help with that.\"");
        Console.WriteLine("Barry: \"$2000. 50% interest per turn. Non negotiable.\"");
        Console.WriteLine("Barry: \"Nice kneecaps by the way.\"");
        Console.Write("\nAccept $2000 from Barry? (yes/no): ");

        if (Console.ReadLine().ToLower() == "yes")
        {
            player.Cash += 2000.00m;
            player.Debt += 2000.00m;
            player.InterestRate = 0.50m;
            Console.WriteLine("Barry hands you a wad of cash. You don't ask where it came from.");
        }
        else
        {
            Console.WriteLine("Barry shrugs. \"I'll be around,\" he says. You don't like the sound of that.");
        }
    }

    private static void NoOneLeft(Player player)
    {
        Console.WriteLine("\nYou've burned every bridge. No one will lend you money.");
        Console.WriteLine("Your parents blocked your number.");
        Console.WriteLine("Grandma changed her will.");
        Console.WriteLine("The bank shredded your file.");
        Console.WriteLine("Barry stopped returning your calls, which is somehow more frightening.");
        Console.WriteLine("\nGAME OVER. You have nothing. Not even Barry wants to know you.");
        player.Cash = 0;
    }
}

