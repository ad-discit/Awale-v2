using System;

namespace Awale_v2
{
    public class Player : Person
    {
        public int PlayerNumber { get; set; }
        public Score PlayerScore { get; set; }
        public string Gamertag { get; set; }

        // Constructor
        public Player(string name, string gamertag, int playerNumber) : base(name)
        {
            PlayerScore = new Score();
            Gamertag = gamertag;
            PlayerNumber = playerNumber;
        }

        public int GetScore()
        {
            return PlayerScore.Total;
        }

        public void UpdateScore(int points)
        {
            PlayerScore.AddPoints(points);
        }
    }
}
