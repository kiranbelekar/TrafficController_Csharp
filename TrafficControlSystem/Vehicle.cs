using System;

namespace TrafficControlSystem
{
    //ASSUMPTIONS: 
    // 1. Vehicles listed in the problem statement are only allowed to go over the 
    // bridge as weights of other vehicles are unknown.
    public class Vehicle
    {
        enum Weights
        {
            motorcycle = 200,
            car = 400,
            bus = 800,
            lorry = 2000,
            tank = 5000
        };

        //Properties
        public int WeightInKgs { get; set; }
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public bool CanEnterBridge { get; set; }

        public Vehicle(string vehicleType, int vehicleId)
        {
            try
            {
                VehicleType = vehicleType;
                WeightInKgs = (int)(Weights)Enum.Parse(typeof(Weights), vehicleType);
                VehicleId = vehicleId;

                if (VehicleType.Equals(ConstantResources.Tank, StringComparison.OrdinalIgnoreCase))
                    CanEnterBridge = false;
                else
                    CanEnterBridge = true;
            }
            catch (ArgumentException)
            {

            }
        }
    }
}
