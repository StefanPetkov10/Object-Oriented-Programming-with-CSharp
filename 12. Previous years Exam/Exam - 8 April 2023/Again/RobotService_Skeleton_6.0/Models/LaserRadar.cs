namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        private const int laserRadarInterfaceStandard = 20_082;
        private const int laserRadarBatteryUsage = 5_000;

        public LaserRadar()
            : base(laserRadarInterfaceStandard, laserRadarBatteryUsage)
        { }
    }

}
