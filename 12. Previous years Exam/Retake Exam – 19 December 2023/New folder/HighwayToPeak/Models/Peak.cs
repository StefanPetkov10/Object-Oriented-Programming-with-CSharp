using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Models
{
    public class Peak : IPeak
    {
        private string name;
        private int elevation;
        private string difficultyLevel;

        public Peak(string name, int elevation, string difficultyLevel)
        {
            this.Name = name;
            this.Elevation = elevation;
            this.DifficultyLevel = difficultyLevel;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.PeakNameNullOrWhiteSpace);
                }
                this.name = value;
            }
        }

        public int Elevation
        {
            // Represents the elevation of the specific peak in meters

            get => this.elevation;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.PeakElevationNegative);
                }
                this.elevation = value;
            }
        }

        public string DifficultyLevel
        {
            //The property is allowed to accept the following options only: "Extreme", "Hard" or "Moderate"
            get => this.difficultyLevel;
            private set
            {
                difficultyLevel = value;
            }
        }

        public override string ToString()
        {
            //"Peak: {Name} -> Elevation: {Elevation}, Difficulty: {DifficultyLevel}"
            return $"Peak: {this.Name} -> Elevation: {this.Elevation}, Difficulty: {this.DifficultyLevel}";
        }
    }
}
