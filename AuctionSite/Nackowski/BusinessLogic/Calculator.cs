using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.BusinessLogic
{
    public class Calculator
    {

        public bool HasBids(int bids)
        {
            if (bids != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int HighestBid(List<int> bids)
        {

            return bids.Max(b => b);

        }
    }
}
