using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrafficControlSystem.UnitTests
{
    [TestClass]
    public class TrafficControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MonitorTraffic_VechileCanEnterBridge_ThrowException()
        {
            TrafficController trafficController = new TrafficController(new MockLoadCalculator(), new ConsoleLogger());
            Vehicle vehicle_tank = new Vehicle(ConstantResources.Tank, 1)
            {
                CanEnterBridge = false
            };
            trafficController.MonitorTraffic(vehicle_tank);
        }

        [TestMethod]
        public void CheckTotalLorriesOnBridge_MoreThan3NotAllowed_ReturnFalse()
        {
            TrafficController trafficController = new TrafficController(new MockLoadCalculator(), new ConsoleLogger());
            trafficController.LorriesOnBridge = new List<Vehicle>
            {
                new Vehicle(ConstantResources.Lorry, 1),
                new Vehicle(ConstantResources.Lorry, 2),
                new Vehicle(ConstantResources.Lorry, 3),           
            };

            Vehicle fourthLorry = new Vehicle(ConstantResources.Lorry, 4);
            Assert.IsFalse(trafficController.CheckTotalLorriesOnBridge(fourthLorry));
        }

    }

    public class MockLoadCalculator : ILoadCalculator
    {
        public int CurrentBridgeThreshold{ get; set; }
        
        public bool CalculateBridgeLoad(Vehicle vehicle, List<Vehicle> waitingListOfVehicles)
        {
            return true;
        }
    }
}
