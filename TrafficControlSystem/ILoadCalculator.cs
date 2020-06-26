using System.Collections.Generic;

namespace TrafficControlSystem
{
    public interface ILoadCalculator
    {
        int CurrentBridgeThreshold { get; set; }
        bool CalculateBridgeLoad(Vehicle vehicle, List<Vehicle> waitingListOfVehicles);
    }
}