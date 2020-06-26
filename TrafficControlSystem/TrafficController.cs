using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TrafficControlSystem
{
    //ASSUMPTIONS: 1. All vehicle's speed is considered constant and will require 10 secs to pass the bridge
    //This speed can be changed at ConstantResources.TimeToCrossBridge

    public class TrafficController
    {
        private readonly ILoadCalculator _loadCalculator;
        private readonly ILogger _logger;

        public List<Vehicle> WaitingListOfVehicles { get; set; }    //vehicles waiting to get on the bridge
        public List<Vehicle> VehiclesOnBridge { get; set; }         //all vehicles over the bridge  
        public List<Vehicle> LorriesOnBridge { get; set; }          //total lorries over the bridge

        public TrafficController(ILoadCalculator loadCalculator, ILogger logger)
        {
            _loadCalculator = loadCalculator;
            _logger = logger;
            WaitingListOfVehicles = new List<Vehicle>();
            VehiclesOnBridge = new List<Vehicle>();
            LorriesOnBridge = new List<Vehicle>(3);
        }

        public void MonitorTraffic(Vehicle vehicle)
        {
            try
            {
                if (!vehicle.CanEnterBridge)
                    throw new InvalidOperationException("An unexpected vehicle was passed which can " +
                                                        "currently not go over the bridge..!!");

                //1. checking if vehicle can be accomodated on the bridge
                bool vehicleAllowed = _loadCalculator.CalculateBridgeLoad(vehicle, WaitingListOfVehicles);

                //BussinesRule: more that 3 lorries not allowed
                if (vehicleAllowed && vehicle.VehicleType.Equals(ConstantResources.Lorry, StringComparison.OrdinalIgnoreCase))
                    vehicleAllowed = CheckTotalLorriesOnBridge(vehicle);

                if (vehicleAllowed)
                    VehiclesOnBridge.Add(vehicle);


                //2. vehicle should go over briged
                if (vehicleAllowed == true)
                {
                    _logger.LogInfo(vehicle.VehicleType + "_" + vehicle.VehicleId + " can go on bridge");
                    AllowVehicle(vehicle);
                    _logger.LogInfo(vehicle.VehicleType + "_" + vehicle.VehicleId + " is sent on bridge");
                }
                //3. not allowed
                else
                {
                    _logger.LogInfo("Your" + vehicle.VehicleType + "_" + vehicle.VehicleId + " " +
                                      "is in waiting list please wait for a while");
                    CheckWaitingList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("ERROR:" + e);
                //throw e;
            }
        }

        public async void AllowVehicle(Vehicle vehicle)
        {
            //1. introduced delay considering time taken buy vehicle to cross bridge
            await Task.Run(new Action(MovingOverBridge));

            //2. removing vehicle from bridge after it crosses the bridge          
            ExitVehicleFormBridge(vehicle);

            if (!VehiclesOnBridge.Any())
                _logger.LogInfo("Bridge clear");
        }

        public static void MovingOverBridge()
        {
            Thread.Sleep(ConstantResources.TimeToCrossBridge);            // wait for 10 seconds     
        }

        public void CheckWaitingList()
        {
            if (WaitingListOfVehicles.Any())
            {
                Vehicle vehicle = WaitingListOfVehicles.FirstOrDefault();

                bool vehicleAllowed = _loadCalculator.CalculateBridgeLoad(vehicle, WaitingListOfVehicles);

                if (vehicleAllowed)
                {
                    VehiclesOnBridge.Add(vehicle);

                    _logger.LogInfo("\tVehicle: " + vehicle.VehicleType + "_" + vehicle.VehicleId
                                    + " from waiting list " + "is allowed to go on bridge");
                    AllowVehicle(vehicle);
                }
            }
        }

        private void ExitVehicleFormBridge(Vehicle vehicle)
        {
            _logger.LogVehicleExit("\tVehicle: " + vehicle.VehicleType + "_" + vehicle.VehicleId
                                    + " exiting bridge");
            VehiclesOnBridge.Remove(vehicle);

            if (vehicle.VehicleType.Equals(ConstantResources.Lorry, StringComparison.OrdinalIgnoreCase))
                LorriesOnBridge.Remove(vehicle);

            //Updating the bridge load after vehicle has crossed the bridge
            var updatedLoad = _loadCalculator.CurrentBridgeThreshold - vehicle.WeightInKgs;
            _loadCalculator.CurrentBridgeThreshold = updatedLoad;

            CheckWaitingList();
        }

        public void CheckLeftoverVehicles()
        {
            if (VehiclesOnBridge.Any())
                _logger.LogInfo("Wait for all vehicles to go");
            else
                _logger.LogInfo("Bridge clear");
        }

        public bool CheckTotalLorriesOnBridge(Vehicle vehicle)
        {
            if (LorriesOnBridge.Count < ConstantResources.MaxLorries)
            {
                LorriesOnBridge.Add(vehicle);
                return true;
            }
            else
            {
                _logger.LogInfo("Currently there are already 3 lorries over bridge.\n" +
                                  "Not more that 3 lorries can go over the bridge.\n" +
                                  "Moving your lorry into the waiting list..please wait!!");

                WaitingListOfVehicles.Add(vehicle);
                return false;
            }
        }
    }
}
