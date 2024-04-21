using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Text;

namespace Handball.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private double rating;
        private string team;
        public Player(string name, double rating)
        {
            Name = name;
            Rating = rating;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.PlayerNameNull);
                }
                name = value;
            }
        }
        //o	The rating of the player. It can be modified through IncreaseRating() and DecreaseRating() methods. Be careful with the access modifier, because the property should be visible for the derived classes.
        public double Rating
        {
            get => rating;
            protected set => rating = value;
        }

        public string Team
        {
            get => team;
            private set => team = value;
        }

        public abstract void DecreaseRating();

        public abstract void IncreaseRating();

        public void JoinTeam(string name)
        {
            this.Team = name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Name}");
            sb.AppendLine($"--Rating: {this.Rating}");

            return sb.ToString().TrimEnd();
        }
    }
}
