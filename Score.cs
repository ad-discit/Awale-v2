using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2

{
    public class Score
    {
        public int Total { get; set; }

        public Score()
        {
            Total = 0;
        }

        // Example Methods
        public void AddPoints(int points)
        {
            Total += points;
        }

        public void ResetScore()
        {
            Total = 0;
        }
    }

}
