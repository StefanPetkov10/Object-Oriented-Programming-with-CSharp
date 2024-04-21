using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        IRepository<ISupplement> supplements;
        IRepository<IRobot> robots;

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;

            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else if (typeName == nameof(IndustrialAssistant))
            {
                robot = new IndustrialAssistant(model);
            }
            else
            {
                throw new ArgumentException(string.Format(OutputMessages.RobotCannotBeCreated, typeName));
            }

            this.robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, model);
        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;

            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == nameof(LaserRadar))
            {
                supplement = new LaserRadar();
            }
            else
            {
                throw new ArgumentException(string.Format(OutputMessages.SupplementCannotBeCreated, typeName));
            }

            this.supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            IEnumerable<IRobot> filteredRobots = robots
                .Models()
                .Where(r => r.InterfaceStandards.Contains(intefaceStandard))
                .OrderByDescending(r => r.BatteryLevel);

            if (filteredRobots.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, serviceName);
            }

            int sumOfTheBatteryLevel = filteredRobots.Sum(r => r.BatteryLevel);

            if (sumOfTheBatteryLevel < totalPowerNeeded)
            {
                return (string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - sumOfTheBatteryLevel));
            }

            else
            {
                int counterRobots = 0;

                foreach (var robot in filteredRobots)
                {
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        robot.ExecuteService(totalPowerNeeded);
                        counterRobots++;
                        break;
                    }
                    else
                    {
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);
                        counterRobots++;
                    }
                }

                return string.Format(OutputMessages.PerformedSuccessfully, serviceName, counterRobots);
            }

        }

        public string Report()
        {
            IEnumerable<IRobot> orderedRobots = robots
           .Models()
           .OrderByDescending(r => r.BatteryLevel)
           .ThenBy(b => b.BatteryCapacity);

            StringBuilder sb = new StringBuilder();

            foreach (IRobot robot in orderedRobots)
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            IEnumerable<IRobot> robotsForFeed = robots
                .Models()
                .Where(r => r.Model == model && r.BatteryCapacity / 2 > r.BatteryLevel);

            foreach (var robot in robotsForFeed)
            {
                robot.Eating(minutes);
            }

            return string.Format(OutputMessages.RobotsFed, robotsForFeed.Count());
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements
            .Models()
            .FirstOrDefault(s => s.GetType().Name == supplementTypeName);

            IRobot robot = robots
                .Models()
                .FirstOrDefault(r => r.Model == model && !r.InterfaceStandards.Contains(supplement.InterfaceStandard));

            robot.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);

        }
    }
}
