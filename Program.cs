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
            MariaDB db = new MariaDB();
            db.testConnection();
            db.createDB();
            db.createPlayersTable();
            db.createPlaysTable();

            db.listTable();

            // Authenticate players
            string player1Gamertag = AuthenticatePlayer(db, 1);
            string player2Gamertag = AuthenticatePlayer(db, 2);

            if (player1Gamertag == null || player2Gamertag == null)
            {
                Console.WriteLine("Authentication failed for one or both players. Exiting game.");
                return;
            }

            // Log initial game session entry for both players
            db.logGamePlay(player1Gamertag, 17); // Log with initial score 50 reward for loigging in 5 times
            db.logGamePlay(player2Gamertag, 5); // Log with initial score 10 testing

            // Initialize players with gamertag
            Player player1 = new Player(player1Gamertag, player1Gamertag, 0); // Player 1 uses row 0
            Player player2 = new Player(player2Gamertag, player2Gamertag, 1); // Player 2 uses row 1

            // Call to list plays after confirming the gamertags
            db.listPlaysForGamertags(player1Gamertag, player2Gamertag);

            Board board = new Board();
            board.SetupBoard();

            Player currentPlayer = player1;
            bool gameOn = true;

            while (gameOn)
            {
                Console.Clear();
                Console.WriteLine($"{currentPlayer.Gamertag}'s turn (Score: {currentPlayer.GetScore()})");
                board.DisplayBoard();

                bool validMove = false;
                while (!validMove)
                {
                    Console.WriteLine("Choose a pit index (1-6):");
                    int pitIndex;
                    string input = Console.ReadLine();

                    // Try parsing the input. If it fails, prompt the user again.
                    try
                    {
                        pitIndex = int.Parse(input) - 1;  // Adjust for zero-based index if necessary
                                                          // Add further validation to check pitIndex range
                        if (pitIndex < 0 || pitIndex >= 6) // Assuming 6 pits
                        {
                            Console.WriteLine("Invalid pit index. It must be between 1 and 6. Please try again.");
                            continue;
                        }
                        board.PlayTurn(currentPlayer, currentPlayer.PlayerNumber, pitIndex);
                        validMove = true;  // If no exception and valid range, move is valid
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    catch (Exception ex)
                    {
                        // Generic catch block to handle other unforeseen exceptions
                        Console.WriteLine("Invalid move: " + ex.Message);
                    }
                }

                // Check if the game is over and log final scores
                if (CheckGameOver(board, player1, player2))
                {
                    db.logGamePlay(player1.Gamertag, player1.GetScore());
                    db.logGamePlay(player2.Gamertag, player2.GetScore());
                    Console.WriteLine("Game over!");
                    Console.WriteLine($"{player1.Gamertag} Score: {player1.GetScore()}");
                    Console.WriteLine($"{player2.Gamertag} Score: {player2.GetScore()}");
                    gameOn = false; // End the game loop
                }

                gameOn = !CheckGameOver(board, player1, player2);
                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }

            Console.WriteLine("Game over!");
            db.logGamePlay(player1.Gamertag, player1.GetScore());
            db.logGamePlay(player2.Gamertag, player2.GetScore());
            Console.WriteLine($"{player1.Gamertag} Score: {player1.GetScore()}");
            Console.WriteLine($"{player2.Gamertag} Score: {player2.GetScore()}");
            Console.ReadKey();
        }

        static string AuthenticatePlayer(MariaDB db, int playerNumber)
        {
            Console.WriteLine($"Player {playerNumber}: Do you need to register a new account? (yes/no)");
            string response = Console.ReadLine().Trim().ToLower();
            if (response == "yes")
            {
                db.createUser();
            }

            Console.WriteLine($"Player {playerNumber}, please login:");
            return db.userLogin(); // Returns gamertag directly after login
        }

        static bool CheckGameOver(Board board, Player player1, Player player2)
        {
            if (player1.GetScore() >= 24 || player2.GetScore() >= 24)
            {
                Console.WriteLine($"Game over: {(player1.GetScore() >= 24 ? player1.Gamertag : player2.Gamertag)} has captured 24 seeds and won!");
                return true;
            }

            if (ArePitsEmpty(board.Rows[player1.PlayerNumber]) || ArePitsEmpty(board.Rows[player2.PlayerNumber]))
            {
                Console.WriteLine("Game over: A player cannot make a move.");
                return true;
            }
            return false;
        }

        static bool ArePitsEmpty(Row row)
        {
            // Returns true if all pits in the row have zero seeds
            return row.Pits.All(pit => pit.Seeds.Count == 0);
        }
    }
}
