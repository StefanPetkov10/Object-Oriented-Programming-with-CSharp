using HighwayToPeak.Models.Contracts;

namespace HighwayToPeak.Models
{
    public class BaseCamp : IBaseCamp
    {
        private List<string> residents;

        public BaseCamp()
        {
            residents = new List<string>();
        }
        public IReadOnlyCollection<string> Residents => residents.AsReadOnly();

        public void ArriveAtCamp(string climberName)
        {
            //A method to record the arrival of a climber at the base camp. It adds the climber's name to the Residents collection.
            residents.Add(climberName);
            residents = residents.OrderBy(x => x).ToList();
        }

        public void LeaveCamp(string climberName)
        {
            residents.Remove(climberName);
        }
    }
}
