using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale_v2
{
    public class Seeds

    {
        public int Count { get; set; }

        public Seeds(int count)
        {
            Count = count;
        }
        
        public void AddSeeds(int additionalSeeds)
        {
            Count += additionalSeeds;
        }

        public void RemoveAllSeeds()
        {
            Count = 0;
        }
    }

}
