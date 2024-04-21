using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> models;

        public LoanRepository()
        {
            this.models = new List<ILoan>();
        }

        public IReadOnlyCollection<ILoan> Models
            => models.AsReadOnly();

        public void AddModel(ILoan model)
        {
            models.Add(model);
        }

        public bool RemoveModel(ILoan model)
             => models.Remove(model);

        public ILoan FirstModel(string typeName)
             => models.FirstOrDefault(m => m.GetType().Name == typeName);


    }
}
