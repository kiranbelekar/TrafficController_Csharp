using System.Collections.Generic;

namespace TrafficControlSystem
{
    public class LoadCalculator: ILoadCalculator
    {
        private int _currentBridgeThreshold;

        public int TotalCapacityInKgs { get; set; }
        public int CurrentBridgeThreshold
        {
            get { return _currentBridgeThreshold; }
            set { _currentBridgeThreshold = value; }
        }

        public LoadCalculator()
        {
            _currentBridgeThreshold = 0;        //initially there is not weight on the bridge
            TotalCapacityInKgs = ConstantResources.MaxBridgeCapacity;
        }

        public bool CalculateBridgeLoad(Vehicle vehicle, List<Vehicle> waitingListOfVehicles)
        {
            int newThreshold = vehicle.WeightInKgs + CurrentBridgeThreshold;

            if (newThreshold <= TotalCapacityInKgs)
            {
                CurrentBridgeThreshold = newThreshold;
                return true;
            }
            else
            {
                waitingListOfVehicles.Add(vehicle);
                return false;
            }          
        }
    }
}
