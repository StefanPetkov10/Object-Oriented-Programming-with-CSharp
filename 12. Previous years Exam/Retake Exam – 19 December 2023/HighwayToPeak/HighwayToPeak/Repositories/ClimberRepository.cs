using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{

    public class ClimberRepository : IRepository<IClimber>
    {
        private List<IClimber> climbers;

        public ClimberRepository()
        {
            climbers = new List<IClimber>();
        }
        public IReadOnlyCollection<IClimber> All => this.climbers;

        public void Add(IClimber model)
        {
            this.climbers.Add(model);
        }

        public IClimber Get(string name)
             => this.climbers.FirstOrDefault(x => x.Name == name);

    }

}

