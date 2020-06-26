namespace TrafficControlSystem
{
    public interface ILogger
    {
        void LogError(string message);
        void LogInfo(string message);
        void LogVehicleExit(string message);
    }
}