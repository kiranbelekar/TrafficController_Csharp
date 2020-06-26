using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficControlSystem
{
    public class ConstantResources
    {
        public const int MaxLorries = 3;
        public const int TimeToCrossBridge = 10000;      //used 10 secs for testing ~(1 sec= 1000)
        public const int MaxBridgeCapacity= 100000;      //used 10000 for testing~ (100000 =1 lakh kgs)

        public const string Lorry = "Lorry";
        public const string Tank = "Tank";

    }
}
