using System;

namespace TrafficControlSystem
{
    public class Program
    {
        private static readonly ILogger _logger = new ConsoleLogger();
        public static void Main()
        {
            try
            {
                TrafficController tc = new TrafficController(new LoadCalculator(), new ConsoleLogger());
                int vehicleID = 0;      //used a simple int value to read logs related to the vehicle movement over bridge,
                                        // in real time scenario this integer will be replace by vehicle number unique to each vehicle

                StartConrollingTraffic(tc, vehicleID);
            }
            catch (Exception)
            {
                _logger.LogError("Invalid input..!!");
                //throw e;
            }
        }

        private static void StartConrollingTraffic(TrafficController tc, int vehicleID)
        {
            _logger.LogInfo("\n Is there a vehicle waiting:(y/n) ");
            var vehicleOnBridge = Convert.ToChar(Console.ReadLine());

            if (!(vehicleOnBridge == 'y' || vehicleOnBridge == 'Y'))
            {
                _logger.LogError("Invalid input..!!");
                Console.ReadLine();
            }

            while (vehicleOnBridge == 'y' || vehicleOnBridge == 'Y')
            {
                vehicleID = vehicleID + 1;
                _logger.LogInfo("\n Enter vehicle type:");
                var vehicleType = Console.ReadLine();
                var vehicle = new Vehicle(vehicleType, vehicleID);

                tc.MonitorTraffic(vehicle);

                _logger.LogInfo("\nIs there a another vehicle waiting:(y/n)");
                vehicleOnBridge = Convert.ToChar(Console.ReadLine());

                //check leftover vehicles on bridge before exiting
                if (!(vehicleOnBridge == 'y' || vehicleOnBridge == 'Y'))
                {
                    tc.CheckLeftoverVehicles();
                    Console.ReadLine();
                }
            }
        }
    }
}
