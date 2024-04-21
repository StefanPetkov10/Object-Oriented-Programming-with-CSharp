using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int convertionCapacityIndex;

        private readonly List<int> interfaceStandards;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            this.Model = model;
            this.BatteryCapacity = batteryCapacity;
            this.BatteryLevel = batteryCapacity;
            this.ConvertionCapacityIndex = convertionCapacityIndex;

            this.interfaceStandards = new List<int>();
        }
        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNullOrWhitespace));
                }
                this.model = value;
            }
        }

        public int BatteryCapacity
        {
            get => this.batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                this.batteryCapacity = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public int ConvertionCapacityIndex { get; private set; }

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();

        public void Eating(int minutes)
        {
            int totalEnergy = minutes * this.ConvertionCapacityIndex;

            if (this.BatteryLevel + totalEnergy <= BatteryCapacity)
            {
                this.BatteryLevel += totalEnergy;
            }
            else
            {
                this.BatteryLevel = batteryCapacity;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }
            return false;

        }
        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{GetType().Name} {this.Model}");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");

            string supplementsInstalled = InterfaceStandards.Any()
                ? string.Join(", ", InterfaceStandards)
                : ("none");

            sb.AppendLine($"--Supplements installed: {supplementsInstalled}");

            return sb.ToString().TrimEnd();
        }
    }
}
