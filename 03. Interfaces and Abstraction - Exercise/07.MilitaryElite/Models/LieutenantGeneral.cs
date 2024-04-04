using _07.MilitaryElite.Models.Interfaces;
using System.Text;

namespace _07.MilitaryElite.Models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, IReadOnlyCollection<IPrivate> privates) 
            : base(id, firstName, lastName, salary)
        {
            Privates = privates;
            //Privates = new List<IPrivate>();
        }

        public IReadOnlyCollection<IPrivate> Privates { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString())
                .AppendLine("Privates:");
            foreach (var currenPrivate in Privates)
            {
                sb.AppendLine($"  {currenPrivate.ToString()}");
            }
            return sb.ToString().Trim();
        }   
    }
}
