﻿
namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public Team(string name)
        {           
            this.name = name;
            this.firstTeam = new List<Person>();
            this.reserveTeam = new List<Person>();
        }

        public IReadOnlyCollection<Person> FirstTeam
            => this.firstTeam.AsReadOnly();
        public IReadOnlyCollection<Person> ReserveTeam
           => this.reserveTeam.AsReadOnly();

        public void AddPlayer(Person person)
        {
            //valid something -> capacity ot etc

            if (person.Age < 40)
            {
                this.firstTeam.Add(person);
            }
            else
            {
                this.reserveTeam.Add(person);
            }
            
        }
    }
}
