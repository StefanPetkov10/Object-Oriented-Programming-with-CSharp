namespace InfluencerManagerApp.Models
{
    public class BloggerInfluencer : Influencer
    {
        private const double engagementRate = 2.0;
        private const double factor = 0.2;

        public BloggerInfluencer(string username, int followers)
            : base(username, followers, engagementRate)
        { }

        public override int CalculateCampaignPrice()
           => (int)Math.Floor(Followers * EngagementRate * factor);

    }
}
