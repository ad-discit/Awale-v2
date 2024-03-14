using Awale_v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2
{

    internal class Program
    {
        static void Main(string[] args)
        {

            // Create a Person
            Person person = new Person("AD");
            Console.WriteLine($"Person Name: {person.Name}");
            Console.ReadKey();

            // Create a Player and set properties
            Player player = new Player("AD2", 1);
            Console.WriteLine($"Player Name: {player.Name}, Player Number: {player.PlayerNumber}");
            Console.ReadKey();

            // Update and get Player's score
            player.UpdateScore(10);
            Console.WriteLine($"Player Score: {player.GetScore()}");
            Console.ReadKey();

            // Create a Score 
            Score score = new Score();
            score.AddPoints(5);
            Console.WriteLine($"Score Total: {score.Total}");
            Console.ReadKey();

            // Initialize the Board
            //Board board = new Board();
            //board.SetupBoard(); // properly initializes the board
            //board.DisplayBoard(); // prints out the board state
            Console.ReadKey();

            // Create Seeds and manipulate count
            Seeds seeds = new Seeds(4);
            seeds.AddSeeds(2);
            Console.WriteLine($"Seeds Count: {seeds.Count}");
            Console.ReadKey();

            // Create a Pit and manipulate seeds
            Pits pit = new Pits(4);
            pit.IncrementSeeds();
            Console.WriteLine($"Pit Seeds Count: {pit.Seeds.Count}");
            Console.ReadKey();

            // Initialize a Row and reset it
            Row row = new Row();
            row.ResetRow(); // resets to initial state
            row.DisplayRow(); // this method prints out the row's state
            Console.ReadKey();

            // Create a Player and set properties
            Player player1 = new Player("AD3", 1);
            Console.WriteLine($"Player Name: {player1.Name}, Player Number: {player1.PlayerNumber}");

            Player player2 = new Player("AD4", 2);
            Console.WriteLine($"Player Name: {player2.Name}, Player Number: {player2.PlayerNumber}");

            // Initialize the Board
            Board board = new Board();
            board.SetupBoard();
            board.DisplayBoard();

            // Simulate a few turns of game
            Console.WriteLine("\nPlayer 1's Turn: Playing from row 0, pit 2");
            board.PlayTurn(player1, 0, 2);
            Console.ReadKey();

            Console.WriteLine("\nPlayer 2's Turn: Playing from row 1, pit 2");
            board.PlayTurn(player2, 1, 2);
            Console.ReadKey();

            // Update and display Player's score (this is just an example and may not reflect the actual game logic)
            player1.UpdateScore(3);
            Console.WriteLine($"Player 1 Score: {player1.GetScore()}");

            player2.UpdateScore(5);
            Console.WriteLine($"Player 2 Score: {player2.GetScore()}");
            Console.ReadKey();


        }
    }
}
