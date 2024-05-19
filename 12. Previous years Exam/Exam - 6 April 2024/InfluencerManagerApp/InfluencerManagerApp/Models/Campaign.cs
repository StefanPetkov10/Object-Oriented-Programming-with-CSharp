using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Utilities.Messages;

namespace InfluencerManagerApp.Models
{
    public abstract class Campaign : ICampaign
    {
        private string brand;
        private double budget;

        private readonly List<string> contributors;
        public Campaign(string brand, double budget)
        {
            this.Brand = brand;
            this.Budget = budget;
            this.contributors = new List<string>();
        }
        public string Brand
        {
            get => this.brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BrandIsrequired));
                }
                this.brand = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set => this.budget = value;
        }

        public IReadOnlyCollection<string> Contributors => contributors.AsReadOnly();

        public void Engage(IInfluencer influencer)
        {
            contributors.Add(influencer.Username);
            budget -= influencer.CalculateCampaignPrice();
        }

        public void Gain(double amount)
        {
            budget += amount;
        }

        public override string ToString()
        {
            return $"{GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {Contributors.Count}";
        }
    }
}
