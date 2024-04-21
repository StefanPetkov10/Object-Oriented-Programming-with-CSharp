using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private List<IPlayer> models;

        public PlayerRepository()
        {
            this.models = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models => models;

        public void AddModel(IPlayer model)
        {
            models.Add(model);
        }

        public bool ExistsModel(string name)//=> models.Any(p => p.Name == name);
        {
            if (models.Any(p => p.Name == name))
            {
                return true;
            }
            return false;
        }

        public IPlayer GetModel(string name)//=> models.FirstOrDefault(p => p.Name == name);
        {
            if (ExistsModel(name))
            {
                return models.FirstOrDefault(p => p.Name == name);
            }
            return null;
        }

        public bool RemoveModel(string name)//this.players.Remove(this.players.FirstOrDefault(p => p.Name == name));
        {
            if (ExistsModel(name))
            {
                models.Remove(GetModel(name));
                return true;
            }
            return false;
        }
    }
}
