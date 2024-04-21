namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        private const int specializedArmInterfaceStandard = 10_045;
        private const int specializedArmBatteryUsage = 10_000;

        public SpecializedArm()
            : base(specializedArmInterfaceStandard, specializedArmBatteryUsage)
        {
        }
    }
}
