using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2
{
    public class Row
    {
        public Pits[] Pits { get; set; }

        public Row()
        {
            Pits = new Pits[6]; //  6 pits 
            for (int i = 0; i < Pits.Length; i++)
            {
                Pits[i] = new Pits(4); // 4 seeds per pit initially (at the start of the game)
            }
        }

        // Methods
        public void ResetRow()
        {
            foreach (var pit in Pits)
            {
                pit.Seeds.Count = 4; // Reset to initial state with 4 seeds per pit
            }
        }

        public void DisplayRow()
        {
            //  displays the row's current state
        }
    }

}
