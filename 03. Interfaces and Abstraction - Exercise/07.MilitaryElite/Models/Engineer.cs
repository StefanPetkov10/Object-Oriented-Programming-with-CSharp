using _07.MilitaryElite.Enums;
using _07.MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.MilitaryElite.Models
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        public Engineer(int id, string firstName, string lastName, decimal salary, Corps corps, IReadOnlyCollection<IRepair> repairs) 
            :base(id, firstName, lastName, salary, corps)
        {
            Pepairs = repairs;
        }
        public IReadOnlyCollection<IRepair> Pepairs { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine(base.ToString());
            sb.AppendLine("Repairs:");

            foreach (var repair in Pepairs)
            {
                sb.AppendLine($"  {repair.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
