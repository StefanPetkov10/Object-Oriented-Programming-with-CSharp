using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class ResourceRepository : IRepository<IResource>
    {
        private readonly List<IResource> models;

        public ResourceRepository()
        {
            this.models = new List<IResource>();
        }

        public IReadOnlyCollection<IResource> Models => models.AsReadOnly();

        public void Add(IResource model)
        {
            models.Add(model);
        }

        public IResource TakeOne(string modelName)
        {
            return models.FirstOrDefault(m => m.GetType().Name == modelName);
        }
    }
}
