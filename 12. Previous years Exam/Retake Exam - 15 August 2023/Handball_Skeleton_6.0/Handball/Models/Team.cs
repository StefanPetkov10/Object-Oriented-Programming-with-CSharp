using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private int pointsEaned;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            this.pointsEaned = 0;
            this.players = new List<IPlayer>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int PointsEarned => this.pointsEaned;

        public double OverallRating
        {
            get
            {
                if (this.Players.Count == 0)
                {
                    return 0;
                }
                return Math.Round(this.players.Average(p => p.Rating), 2);
            }
        }

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();

        public void Draw()
        {
            this.pointsEaned += 1;
            this.Players.FirstOrDefault(p => p.GetType().Name == nameof(Goalkeeper)).IncreaseRating();
        }

        public void Lose()
        {
            foreach (var player in this.players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            this.players.Add(player);
        }

        public void Win()
        {
            this.pointsEaned += 3;
            foreach (var player in this.players)
            {
                player.IncreaseRating();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team: {this.Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            //--Players: {name1}, {name2}…/none"
            sb.Append($"--Players: ");

            if (this.Players.Any())
            {
                var names = this.Players.Select(p => p.Name);
                sb.Append(string.Join(", ", names));
            }
            else
            {
                sb.Append("none");
            }

            return sb.ToString().TrimEnd();


        }
    }
}
