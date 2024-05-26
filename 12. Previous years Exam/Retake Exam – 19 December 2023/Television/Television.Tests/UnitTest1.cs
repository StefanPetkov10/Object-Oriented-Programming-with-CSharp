namespace Television.Tests
{
    using NUnit.Framework;

    public class Tests
    {
        private TelevisionDevice televisionDevice;
        [SetUp]
        public void Setup()
        {
            televisionDevice = new TelevisionDevice("Samsung", 1000, 1920, 1080);

        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual("Samsung", televisionDevice.Brand);
            Assert.AreEqual(1000, televisionDevice.Price);
            Assert.AreEqual(1920, televisionDevice.ScreenWidth);
            Assert.AreEqual(1080, televisionDevice.ScreenHeigth);
        }

        [Test]
        public void TestProperties()
        {
            int expectedChannel = 0;
            int expectedVolume = 13;
            bool expectedMuted = false;

            Assert.AreEqual(expectedChannel, televisionDevice.CurrentChannel);
            Assert.AreEqual(expectedVolume, televisionDevice.Volume);
            Assert.AreEqual(expectedMuted, televisionDevice.IsMuted);
        }

        [Test]
        public void TestSwitchOn()
        {
            string expected = "Cahnnel 0 - Volume 13 - Sound On";
            Assert.AreEqual(expected, televisionDevice.SwitchOn());
        }
        [Test]
        public void TestSwitchOnMuted()
        {
            string expected = "Cahnnel 0 - Volume 13 - Sound Off";
            televisionDevice.MuteDevice();
            Assert.AreEqual(expected, televisionDevice.SwitchOn());
        }

        [TestCase]
        public void TestChangeChannel()
        {
            int expectedChannel = -1;
            Assert.Throws<System.ArgumentException>(()
                => televisionDevice.ChangeChannel(expectedChannel));
        }


        [Test]
        public void TestChangeChanelReturnTheCurrentChannel()
        {
            int expectedChannel = 1;
            Assert.AreEqual(expectedChannel, televisionDevice.ChangeChannel(expectedChannel));
        }
        [Test]


        [TestCase("UP")]
        public void TestVolumeChange(string direction)
        {
            int expectedVolume = 23;
            string expectedReturn = $"Volume: {expectedVolume}";
            televisionDevice.VolumeChange(direction, 10);
            Assert.AreEqual(expectedVolume, televisionDevice.Volume);
            expectedVolume = 33;
            expectedReturn = $"Volume: {expectedVolume}";
            Assert.AreEqual(expectedReturn, televisionDevice.VolumeChange(direction, 10));

        }
        [TestCase("UP")]
        public void TestVolumeChangeUp(string direction)
        {
            int expectedVolume = 100;
            string expectedReturn = $"Volume: {expectedVolume}";
            televisionDevice.VolumeChange(direction, 100);
            Assert.AreEqual(expectedVolume, televisionDevice.Volume);
            Assert.AreEqual(expectedReturn, televisionDevice.VolumeChange(direction, 100));
        }

        [TestCase("DOWN")]
        public void TestVolumeChangeDown(string direction)
        {
            int expectedVolume = 3;
            string expectedReturn = $"Volume: {expectedVolume}";
            televisionDevice.VolumeChange(direction, 10);
            Assert.AreEqual(expectedVolume, televisionDevice.Volume);
            expectedVolume = 0;
            expectedReturn = $"Volume: {expectedVolume}";
            Assert.AreEqual(expectedReturn, televisionDevice.VolumeChange(direction, 10));
        }
        [TestCase("DOWN")]
        public void TestVolumeChangeDownToZero(string direction)
        {
            int expectedVolume = 0;
            string expectedReturn = $"Volume: {expectedVolume}";
            televisionDevice.VolumeChange(direction, 100);
            Assert.AreEqual(expectedVolume, televisionDevice.Volume);
            Assert.AreEqual(expectedReturn, televisionDevice.VolumeChange(direction, 100));
        }

        [Test]
        public void TestMuteDevice()
        {
            bool expectedMuted = true;
            Assert.AreEqual(expectedMuted, televisionDevice.MuteDevice());
            expectedMuted = false;
            Assert.AreEqual(expectedMuted, televisionDevice.MuteDevice());
        }

        [Test]
        public void TestToString()
        {
            string expectedBrand = "Samsung";
            int expectedPrice = 1000;
            int expectedWidth = 1920;
            int expectedHeight = 1080;

            string result = televisionDevice.ToString();

            // Assert
            string expected = $"TV Device: {expectedBrand}, Screen Resolution: {expectedWidth}x{expectedHeight}, Price {expectedPrice}$";
            Assert.AreEqual(expected, result);
        }


    }

}
