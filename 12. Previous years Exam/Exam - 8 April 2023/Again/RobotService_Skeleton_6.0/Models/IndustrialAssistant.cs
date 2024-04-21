namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        private const int batteryCapacity = 40_000;
        private const int conversionCapacityIndex = 5_000;
        public IndustrialAssistant(string model)
            : base(model, batteryCapacity, conversionCapacityIndex)
        {
        }
    }
}
