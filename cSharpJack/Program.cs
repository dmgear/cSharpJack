using System;

public class Card
{
    public string Rank { get; private set; }
    public string Suit { get; private set; }

    public Card(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }

    public static Dictionary<string, int> card_values = new Dictionary<string, int>
    {
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"10", 10},
        {"J", 10},
        {"Q", 10},
        {"K", 10},
        {"A", 11}
    };
}

public class Player 
{
    public int score = 0;

    public virtual string name { get; } = "Player";

    public List<Card> hand = new List<Card>();

    public void hitMe(Deck deck)
    {     
        hand.Add(deck.Cards[0]);
        deck.Cards.Remove(deck.Cards[0]);
    }

    public void CalculateScore()
    {
        score = 0;
        foreach (var item in hand)
        {
            if (Card.card_values.ContainsKey(item.Rank))
            {
                score += Card.card_values[item.Rank];
            }
        }
    }

    public void ShowHand()
    {
        Console.WriteLine($"Cards in {name}'s hand: " + string.Join(", ",  hand));
    }
}

public class Computer : Player
{
    public override string name { get; } = "Computer";
}

public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    public Deck()
    {
        ConstructDeck();
        Shuffle();
    }

    public void ConstructDeck()
    {
        string[] suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                Cards.Add(new Card(rank, suit));
            }
        }
    }

    public void Shuffle()
      {
        int n = Cards.Count - 1;
        Random random = new Random();

        while (n > 0)
        {
             int k = random.Next(n+1);
             Card temp = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = temp;
            n--;
        }
      }
}

public class BlackjackGame
{

    public static void Deal(Player player, Computer computer, Deck deck)
    {
        for (int i = 0; i < 2; i++)
        {
            player.hitMe(deck);
            computer.hitMe(deck);
        }
    }

    public bool Winner(Player player, Computer computer)
      {
            if (player.score == 21)
            
                  return true;
            
            else if (player.score > 21)
            
                  return false;

            else if (player.score > computer.score)
            
                  return true;
            
            else if (player.score < computer.score)
            
                  return false;
            
            else
            
                  return false;
            
            
      }

    public void PlayGame(Player player, Computer computer, Deck deck)
    {
        Deal(player, computer, deck);
        Console.WriteLine("Welcome to CsharpJack!\nTo win, get your total card value to or as close to 21 as possible.");
        player.CalculateScore();
        computer.CalculateScore();
        while (true)
        {
            player.ShowHand();

            string input = Console.ReadLine();

            if (input == "hit")
            {
                player.hitMe(deck);
                computer.hitMe(deck);
                player.ShowHand();
            }

            else if (input == "stand")
            {
                if (Winner(player, computer))
                
                    Console.WriteLine("Congrats! You won!");
                
                else
                
                    Console.WriteLine("Better luck next time!");
                
                break;
            }

            else 

                Console.WriteLine("Better luck next time!");

            player.CalculateScore();
            computer.CalculateScore();   

            if (player.score > 21)
            {
                Console.WriteLine("Bust! Better luck next time!");
                break;
            }
            else if (computer.score > 21)
            {
                Console.WriteLine("The computer busted! You win!");
                break;
            }
        }
    }
}
class Program
{
    static void Main()
    {
        Deck myDeck = new Deck();
        Player newPlayer = new Player();
        Computer newComputer = new Computer();
        BlackjackGame newGame = new BlackjackGame();
        newGame.PlayGame(newPlayer, newComputer, myDeck);
    }
}