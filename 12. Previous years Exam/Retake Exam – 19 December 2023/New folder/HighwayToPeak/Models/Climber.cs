using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System.Text;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private List<string> conqueredPeaks;

        public Climber(string name, int stamina)
        {
            this.Name = name;
            this.Stamina = stamina;
            this.conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                this.name = value;
            }
        }
        public int Stamina
        {
            //If it exceeds 10 during any operation, it should be reset to 10.
            //If it drops below zero during any operation, it should be reset to zero.
            get => this.stamina;
            protected set
            {
                if (value < 0)
                {
                    this.stamina = 0;
                }
                else if (value > 10)
                {
                    this.stamina = 10;
                }
                else
                {
                    this.stamina = value;
                }
            }
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks;

        public void Climb(IPeak peak)
        {
            if (!conqueredPeaks.Contains(peak.Name))
            {
                this.conqueredPeaks.Add(peak.Name);
            }

            int tempStamina = 0;

            if (peak.DifficultyLevel == "Extreme")
            {
                tempStamina += 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                tempStamina += 4;
            }
            else
            {
                tempStamina += 2;
            }

            this.Stamina -= tempStamina;
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name} - Name: {this.Name}, Stamina: {this.Stamina}");
            if (this.ConqueredPeaks.Count == 0)
            {
                sb.AppendLine("Peaks conquered: no peaks conquered");
            }
            else
            {
                sb.AppendLine($"Peaks conquered: {conqueredPeaks.Count}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
