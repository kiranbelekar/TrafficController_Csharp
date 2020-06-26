using System;

namespace TrafficControlSystem
{
    public class ConsoleLogger : ILogger
    {

        public void LogError(string message)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void LogInfo(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
        }

        public void LogVehicleExit(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}