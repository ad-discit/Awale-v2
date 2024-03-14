using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2
{
    public class Pits
    {
        public Seeds Seeds { get; set; }

        public Pits(int initialSeedCount)
        {
            Seeds = new Seeds(initialSeedCount);
        }

        // Methods
        public void IncrementSeeds()
        {
            Seeds.AddSeeds(1);
        }

        public void CaptureSeeds()
        {
            Seeds.RemoveAllSeeds();
        }
    }

}
