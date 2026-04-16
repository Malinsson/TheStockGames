# Big Stonks Simulator 3000

Last Tuesday you reversed your car into a statue of Barry Henderson, beloved local businessman and surprisingly sensitive man. Barry is suing you for $10,000 — not for the statue, but for the emotional distress caused to his pet tortoise, Gerald. Gerald used to love lettuce. Now he just stares.

You have $200, an expired Subway card, and a lawyer found on a lamppost. His only advice: *"Have you considered the stock market?"*

You have not. But you're considering it now.

---

## What is this?

Big Stonks Simulator 3000 is a CLI-based stock trading game built in C#. Buy and sell stocks, survive random market events, dodge personal financial disasters, and try to reach $10,000 before Barry takes everything you own — including, somehow, your microwave.

---

## Features

- **Live market** — 5 stocks with randomised price swings each turn
- **Breaking news** — random market events that send prices soaring or crashing
- **Personal events** — bad things happen to you specifically, because of course they do
- **Bankruptcy system** — go broke and borrow from your parents, grandparents, the bank, or a man named Barry (different Barry)
- **Win/lose conditions** — reach $10,000 to pay off Gerald's emotional damages, or lose everything and have Barry take your microwave
- **Portfolio tracker** — see your holdings, current value, and profit/loss at a glance

---

## How to play

Run the project and enter your name. You start with $200.

| Command     | Action                        |
|-------------|-------------------------------|
| `buy`       | Buy shares in a stock         |
| `sell`      | Sell shares you own           |
| `portfolio` | View your current holdings    |
| `quit`      | Give up (Barry wins)          |

Each turn the market updates, random events may fire, and your debt accrues interest if you've had to borrow money. Try to grow your $200 into $10,000 before you run out of turns, cash, and dignity.

---

## Project structure

```
WallStreetSimulator/
├── Program.cs              # Entry point
├── GameEngine.cs           # Main game loop and command handling
├── Market.cs               # Holds and updates all stocks
├── Stock.cs                # Individual stock with price logic
├── Player.cs               # Player cash, holdings, and debt
├── EventSystem.cs          # Random stock market events
├── PlayerEventSystem.cs    # Random personal financial disasters
├── BankruptcyHandler.cs    # Escalating borrow dialogue
├── CLIRenderer.cs          # All console output and formatting
└── ScoreManager.cs         # Save and load final scores
```

---

## Built with

- C# / .NET
- `System.Text.Json` for score saving
- `Console` for UI rendering
- Genuine concern for Gerald's wellbeing

---

## Known issues

- Gerald has still not eaten. Lettuce pending.
