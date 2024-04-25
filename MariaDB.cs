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
        ///string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP";
        string myConnectorString = "server=localhost;user=root;password=AD50GDUSP";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(myConnectorString);
        }

        public void testConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(myConnectorString);
                conn.Open();
                Console.WriteLine("ok");
                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, conn);
                var version = cmd.ExecuteScalar().ToString();
                Console.WriteLine($"MariaDB version: {version}");


            }
            catch (Exception ex) {}
            
        }
        public void createDB()
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;";
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "CREATE DATABASE IF NOT EXISTS awalegameproject;";
                    var response = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Response: {response}");
                }
            }
        }

        public void tryCreateAlterTable()
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject"; 
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS players (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(255));";
                    var response = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Response: {response}");

                    //cmd.CommandText = "ALTER TABLE players ADD COLUMN surname VARCHAR(255);";
                    //response = cmd.ExecuteNonQuery();
                    //Console.WriteLine($"Response: {response}");
                }
            }
        }

        public void createUser(string playerName, string playerSurname)
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "INSERT INTO players (name, surname) VALUES (@name, @surname);";
                    cmd.Parameters.AddWithValue("@name", playerName);
                    cmd.Parameters.AddWithValue("@surname", playerSurname);
                    var response = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Response: {response}");
                    conn.Close();
                }
            }
        }

        public void listTable()
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            try
            {
                using (var connection = new MySqlConnection(connStr))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM players", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming the players table has columns 'id', 'name', and 'surname'
                                Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Surname: {reader["surname"]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void deletePlayer(string playerName)
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            using (var conn = new MySqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "DELETE FROM players WHERE name = @name";
                    cmd.Parameters.AddWithValue("@name", playerName);
                    var response = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Response: {response}");
                }
            }
        }

        public void checkUser(string username, string password)
        {
            string connStr = "server=localhost;user=root;port=3306;password=AD50GDUSP;database=awalegameproject";
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT username FROM players WHERE username = @username AND password = @password";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine(reader["username"]);
                            }
                            else
                            {
                                Console.WriteLine("User does not exist or incorrect password");
                            }
                        } // Reader is closed and disposed here.

                    }
                } // Connection is closed and disposed here, due to the using statement.
            }
            catch (Exception ex)
            {
                // Handle exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }






    }
}
