using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TrafficControlSystem.UnitTests
{
    [TestClass]
    public class LoadCalculatorTests
    {   
        [TestMethod]
        public void CalculateBridgeLoad_IncomingVehicleCanBeAccomodated_ReturnTrue()
        {
            LoadCalculator loadCalculator = new LoadCalculator()
            {
                CurrentBridgeThreshold = 95,
                TotalCapacityInKgs = 500,
            };
           
            Vehicle car = new Vehicle("car", 6);
            List<Vehicle> waitingVehiclesList = new List<Vehicle>();     
            Assert.IsTrue(loadCalculator.CalculateBridgeLoad(car, waitingVehiclesList));
        }


        [TestMethod]
        public void CalculateBridgeLoad_IncomingVehicleCannotBeAccomodated_ReturnTrue()
        {
            LoadCalculator loadCalculator = new LoadCalculator()
            {
                CurrentBridgeThreshold = 200,
                TotalCapacityInKgs = 700,
            };

            Vehicle fourthLorry = new Vehicle("bus", 3);
            List<Vehicle> waitingVehiclesList = new List<Vehicle>();
            Assert.IsFalse(loadCalculator.CalculateBridgeLoad(fourthLorry, waitingVehiclesList));
        }

    }
}
