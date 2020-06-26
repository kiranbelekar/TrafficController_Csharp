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
        public const int TimeToCrossBridge = 10000;      //todo: remove later(1 sec= 1000)
        public const int MaxBridgeCapacity= 10000;      //todo: remove later : one lakh kgs ~ 100 tonnes (100000 1 lakh kgs)

        public const string Lorry = "Lorry";
        public const string Tank = "Tank";

    }
}
