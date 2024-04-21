using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System.Text;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private List<string> catchedFish;
        private double competitionPoints;
        private bool hasHealthIssues;

        protected Diver(string name, int oxygenLevel)
        {
            this.Name = name;
            this.OxygenLevel = oxygenLevel;
            this.catchedFish = new List<string>();
            this.competitionPoints = 0;
            this.hasHealthIssues = false;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DiversNameNull));
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (value < 0)
                {
                    oxygenLevel = 0;
                }
                else
                {
                    oxygenLevel = value;
                }

            }
        }

        public IReadOnlyCollection<string> Catch => catchedFish;

        public double CompetitionPoints => competitionPoints;

        public bool HasHealthIssues => hasHealthIssues;

        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            catchedFish.Add(fish.Name);
            competitionPoints += fish.Points;
        }

        public abstract void Miss(int TimeToCatch);

        public abstract void RenewOxy();

        public void UpdateHealthStatus() //!
        {
            hasHealthIssues = !hasHealthIssues;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints:f1}: ]");
            return sb.ToString().TrimEnd();
        }
    }
}
