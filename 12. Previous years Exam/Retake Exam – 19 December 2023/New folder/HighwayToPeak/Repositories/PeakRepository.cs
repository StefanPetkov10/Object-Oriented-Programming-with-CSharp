using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository : IRepository<IPeak>
    {
        private List<IPeak> peaks;
        public PeakRepository()
        {
            peaks = new List<IPeak>();
        }
        public IReadOnlyCollection<IPeak> All => this.peaks.AsReadOnly();

        public void Add(IPeak model)
        {
            this.peaks.Add(model);
        }

        public IPeak Get(string name)
             => this.peaks.FirstOrDefault(x => x.Name == name);



    }
}
