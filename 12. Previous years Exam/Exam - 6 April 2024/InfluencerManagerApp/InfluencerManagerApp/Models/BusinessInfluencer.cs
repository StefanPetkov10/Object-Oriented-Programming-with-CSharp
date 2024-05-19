namespace InfluencerManagerApp.Models
{
    public class BusinessInfluencer : Influencer
    {
        private const double engagementRate = 3.0;
        private const double factor = 0.15;

        public BusinessInfluencer(string username, int followers)
            : base(username, followers, engagementRate)
        { }

        public override int CalculateCampaignPrice()
           => (int)Math.Floor(Followers * EngagementRate * factor);

    }
}
