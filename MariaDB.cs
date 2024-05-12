using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySqlConnector;
///string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP";
///using (var conn = new MySqlConnection(connStr)) ;
//using (var cmd = conn.CreateCommand()) ;


namespace Awale_v2
{
    public class MariaDB
    {
        private string myConnectorString = "server=localhost;user=root;password=AD50GDUSP;database=awalegameproject";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(myConnectorString);
        }

        public void testConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    Console.WriteLine("Database connection successful.");

                    var stm = "SELECT VERSION()";
                    using (var cmd = new MySqlCommand(stm, conn))
                    {
                        var version = cmd.ExecuteScalar().ToString();
                        Console.WriteLine($"MariaDB version: {version}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open connection: {ex.Message}");
            }
        }

        public void createDB()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CREATE DATABASE IF NOT EXISTS awalegameproject;";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Database created or already exists.");
                }
            }
        }

        public void createPlayersTable()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS players (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        name VARCHAR(50) NOT NULL,
                        surname VARCHAR(255) NOT NULL,
                        gamertag VARCHAR(255) NOT NULL UNIQUE,
                        password VARCHAR(255) NOT NULL
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table 'players' created or already exists.");
                }
            }
        }

        public bool createUser()
        {
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter gamertag:");
            string gamertag = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM players WHERE gamertag = @gamertag", conn))
                {
                    checkCmd.Parameters.AddWithValue("@gamertag", gamertag);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (exists > 0)
                    {
                        Console.WriteLine("This gamertag is already taken. Please choose another one.");
                        return false;
                    }
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO players (name, surname, gamertag, password) VALUES (@name, @surname, @gamertag, @password);";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@gamertag", gamertag);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("User created successfully.");
                    return true;
                }
            }
        }

        public void listTable()
        {
            Console.WriteLine("Press 'L' to list all players or any other key to cancel.");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            if (key.KeyChar != 'L' && key.KeyChar != 'l')
            {
                Console.WriteLine("Listing canceled.");
                return;
            }

            using (var conn = new MySqlConnection(myConnectorString))
            {
                conn.Open();
                using (var command = new MySqlCommand("SELECT * FROM players", conn))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Surname: {reader["surname"]}");
                    }
                }
            }
        }

        public void deletePlayer(string gamerTag)
        {
            using (var conn = new MySqlConnection(myConnectorString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM players WHERE gamertag = @gamertag";
                    cmd.Parameters.AddWithValue("@gamertag", gamerTag);
                    var response = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Response: {response} player(s) deleted.");
                }
            }
        }

        public bool checkUser(string gamertag, string password)
        {
            using (var conn = new MySqlConnection(myConnectorString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT password FROM players WHERE gamertag = @gamertag", conn))
                {
                    cmd.Parameters.AddWithValue("@gamertag", gamertag);
                    var dbPassword = cmd.ExecuteScalar()?.ToString();
                    if (dbPassword != null && dbPassword == password)
                    {
                        Console.WriteLine("Authentication successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Authentication failed.");
                        return false;
                    }
                }
            }
        }

        public string userLogin()
        {
            Console.WriteLine("Enter your gamertag:");
            string gamertag = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT password FROM players WHERE gamertag = @gamertag", conn))
                {
                    cmd.Parameters.AddWithValue("@gamertag", gamertag);
                    var dbPassword = cmd.ExecuteScalar()?.ToString();
                    if (dbPassword != null && dbPassword == password)
                    {
                        Console.WriteLine("Login successful.");
                        return gamertag;
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Incorrect gamertag or password.");
                        return null;
                    }
                }
            }
        }

        public void createPlaysTable()
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS plays (
                id INT AUTO_INCREMENT PRIMARY KEY,
                gamertag VARCHAR(255) NOT NULL,
                play_date DATETIME NOT NULL,
                score INT NOT NULL,
                games_played INT NOT NULL DEFAULT 1
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table 'plays' created or already exists.");
                }
            }
        }

        public void logGamePlay(string gamertag, int score)
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            INSERT INTO plays (gamertag, play_date, score, games_played) 
            VALUES (@gamertag, NOW(), @score, 1)
            ON DUPLICATE KEY UPDATE 
                score = VALUES(score),
                games_played = games_played + 1;";
                    cmd.Parameters.AddWithValue("@gamertag", gamertag);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Game play logged successfully.");
                }
            }
        }

        public void listPlaysForGamertags(string gamertag1, string gamertag2)
        {
            Console.WriteLine("Press 'M' to list game plays for the current players or any other key to cancel.");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();  // Move to the next line after key input

            if (key.KeyChar != 'M' && key.KeyChar != 'm')
            {
                Console.WriteLine("Listing canceled.");
                return;
            }

            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"
                SELECT gamertag, play_date, score
                FROM plays
                WHERE gamertag = @gamertag1 OR gamertag = @gamertag2
                ORDER BY play_date DESC;";  // Fetches recent plays for both players

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@gamertag1", gamertag1);
                        cmd.Parameters.AddWithValue("@gamertag2", gamertag2);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Console.WriteLine($"Listing plays for {gamertag1} and {gamertag2}:");
                                while (reader.Read())
                                {
                                    string gt = reader["gamertag"].ToString();
                                    DateTime playDate = DateTime.Parse(reader["play_date"].ToString());
                                    int score = Convert.ToInt32(reader["score"]);
                                    Console.WriteLine($"Gamertag: {gt}, Date: {playDate}, Score: {score}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No game plays found for the specified players.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing game plays: {ex.Message}");
            }
        }


    }
}

