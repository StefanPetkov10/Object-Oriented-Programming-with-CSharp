namespace Railway.Tests
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        private RailwayStation station;
        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("Sofia");
        }

        [Test]
        public void TestConstructor()
        {
            string expectedName = "Sofia";
            Assert.AreEqual(expectedName, station.Name);
            Assert.NotNull(station.ArrivalTrains);
            Assert.NotNull(station.DepartureTrains);
        }

        [Test]
        public void Constructor_WithNullOrEmptyName_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => station = new RailwayStation(null));
            Assert.Throws<ArgumentException>(() => station = new RailwayStation(""));
            Assert.Throws<ArgumentException>(() => station = new RailwayStation(" "));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void StantionNameShouldThrowExceptionIfIsSetToNull(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => new RailwayStation(name));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [Test]
        public void TestNewArrivalOnBoard()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            Assert.AreEqual(3, station.ArrivalTrains.Count);
        }

        [Test]
        public void TestTrainHasArrived()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            string expected = "There are other trains to arrive before Sofia - Plovdiv.";
            string actual = station.TrainHasArrived("Sofia - Plovdiv");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTrainHasArrivedShouldMoveTrainToDepartureTrains()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            station.TrainHasArrived("Sofia - Varna");

            Assert.AreEqual(2, station.ArrivalTrains.Count);
            Assert.AreEqual(1, station.DepartureTrains.Count);
        }

        [Test]
        public void TestTrainHasArrivedShouldReturnCorrectMessage()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            string expected = "Sofia - Varna is on the platform and will leave in 5 minutes.";
            string actual = station.TrainHasArrived("Sofia - Varna");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTrainHasLeftShouldReturnTrueIfTrainHasLeft()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            station.TrainHasArrived("Sofia - Varna");

            bool actual = station.TrainHasLeft("Sofia - Varna");

            Assert.IsTrue(actual);
        }

        [Test]
        public void TestTrainHasLeftShouldMoveTrainToDepartureTrains()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            station.TrainHasArrived("Sofia - Varna");

            station.TrainHasLeft("Sofia - Varna");

            Assert.AreEqual(2, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TestTrainHasLeftShouldReturnFalseIfTrainHasNotLeft()
        {
            station.NewArrivalOnBoard("Sofia - Varna");
            station.NewArrivalOnBoard("Sofia - Plovdiv");
            station.NewArrivalOnBoard("Sofia - Burgas");

            station.TrainHasArrived("Sofia - Varna");

            bool actual = station.TrainHasLeft("Sofia - Plovdiv");

            Assert.IsFalse(actual);
        }

    }
}
