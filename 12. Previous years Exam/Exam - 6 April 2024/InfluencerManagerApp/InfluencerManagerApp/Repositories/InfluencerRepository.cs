using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;

namespace InfluencerManagerApp.Repositories
{
    public class InfluencerRepository : IRepository<IInfluencer>
    {
        private readonly List<IInfluencer> influencers;

        public InfluencerRepository()
        {
            this.influencers = new List<IInfluencer>();
        }
        public IReadOnlyCollection<IInfluencer> Models => influencers.AsReadOnly();

        public void AddModel(IInfluencer model)
        {
            influencers.Add(model);
        }

        public IInfluencer FindByName(string name)
            => influencers.FirstOrDefault(i => i.Username == name);

        public bool RemoveModel(IInfluencer model)
            => influencers.Remove(model);
    }
}
