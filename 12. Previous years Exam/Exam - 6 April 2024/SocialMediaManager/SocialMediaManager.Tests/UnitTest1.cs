namespace SocialMediaManager.Tests
{
    public class Tests
    {
        private InfluencerRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new InfluencerRepository();
        }

        [Test]
        public void RegisterInfluencer_ValidInfluencer_SuccessfullyAdded()
        {
            var influencer = new Influencer("john_doe", 10000);
            string result = repository.RegisterInfluencer(influencer);
            Assert.AreEqual($"Successfully added influencer {influencer.Username} with {influencer.Followers}", result);
            Assert.AreEqual(1, repository.Influencers.Count);
        }

        [Test]
        public void RegisterInfluencer_NullInfluencer_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => repository.RegisterInfluencer(null));
        }

        [Test]
        public void RegisterInfluencer_DuplicateUsername_ExceptionThrown()
        {
            var influencer1 = new Influencer("john_doe", 10000);
            repository.RegisterInfluencer(influencer1);
            var influencer2 = new Influencer("john_doe", 5000);
            Assert.Throws<InvalidOperationException>(() => repository.RegisterInfluencer(influencer2));
        }

        [Test]
        public void RemoveInfluencer_ExistingUsername_ReturnsTrueAndRemovesInfluencer()
        {
            var influencer = new Influencer("john_doe", 10000);
            repository.RegisterInfluencer(influencer);
            bool isRemoved = repository.RemoveInfluencer("john_doe");
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(0, repository.Influencers.Count);
        }

        [Test]
        public void RemoveInfluencer_NonexistentUsername_ReturnsFalse()
        {
            bool isRemoved = repository.RemoveInfluencer("nonexistent_user");
            Assert.IsFalse(isRemoved);
        }

        [Test]
        public void RemoveInfluencer_NullOrEmptyUsername_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => repository.RemoveInfluencer(null));
            Assert.Throws<ArgumentNullException>(() => repository.RemoveInfluencer(""));
        }

        [Test]
        public void GetInfluencerWithMostFollowers_ReturnsInfluencerWithMostFollowers()
        {
            var influencer1 = new Influencer("john_doe", 10000);
            var influencer2 = new Influencer("jane_smith", 15000);
            repository.RegisterInfluencer(influencer1);
            repository.RegisterInfluencer(influencer2);
            Influencer result = repository.GetInfluencerWithMostFollowers();
            Assert.AreEqual(influencer2, result);
        }

        [Test]
        public void GetInfluencer_ValidUsername_ReturnsInfluencer()
        {
            var influencer = new Influencer("john_doe", 10000);
            repository.RegisterInfluencer(influencer);
            Influencer result = repository.GetInfluencer("john_doe");
            Assert.AreEqual(influencer, result);
        }

        [Test]
        public void GetInfluencer_NonexistentUsername_ReturnsNull()
        {
            Influencer result = repository.GetInfluencer("nonexistent_user");
            Assert.IsNull(result);
        }
    }
}

