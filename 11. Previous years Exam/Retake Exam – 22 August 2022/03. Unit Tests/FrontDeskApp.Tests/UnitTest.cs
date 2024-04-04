using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Hotel hotel;
        [SetUp]
        public void Setup()
        {
            hotel = new Hotel("Hotel", 5);
        }

        [Test]
        public void TestConstructor()
        {
            string expectedName = "Hotel";
            int expectedCategory = 5;

            Assert.AreEqual(expectedName, hotel.FullName);
            Assert.AreEqual(expectedCategory, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            //Assert.That(hotel.Turnover.Equals(0));
            Assert.NotNull(hotel.Rooms);
            Assert.NotNull(hotel.Bookings);
        }

        [Test]
        public void Constructor_WithNullOrEmptyName_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(null, 4));
            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel("", 4));
            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(" ", 4));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void HotelNameShouldThrowExceptionIfIsSetToNull(string name)
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(()
                               => new Hotel(name, 4));
            string expectedMessage = "Value cannot be null.";

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(0)]
        [TestCase(6)]
        public void HotelCategoryShouldThrowExceptionIfIsSetToInvalidValue(int category)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
            => new Hotel("Hotel", category));
            string expectedMessage = "Value does not fall within the expected range."
            ;

            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase(4)]
        [TestCase(5)]
        public void HotelCategoryShouldNotThrowExceptionIfIsSetToValidValue(int expectedCategory)
        {
            hotel = new Hotel("Hotel", expectedCategory);

            Assert.AreEqual(expectedCategory, hotel.Category);
        }

        [Test]
        public void TestAddRoom()
        {
            Room room = new Room(2, 100);
            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        public void TestRoomConstructor()
        {
            int expectedBedCapacity = 2;
            double expectedPricePerNight = 100;

            Room room = new Room(expectedBedCapacity, expectedPricePerNight);

            Assert.AreEqual(expectedBedCapacity, room.BedCapacity);
            Assert.AreEqual(expectedPricePerNight, room.PricePerNight);
        }

        [Test]
        public void TestRoomConstructorShouldThrowExceptionIfBedCapacityIsSetToInvalidValue()
        {
            Assert.Throws<ArgumentException>(() => new Room(0, 100));
            Assert.Throws<ArgumentException>(() => new Room(-1, 100));
        }

        [Test]
        public void TestRoomConstructorShouldThrowExceptionIfPricePerNightIsSetToInvalidValue()
        {
            Assert.Throws<ArgumentException>(() => new Room(2, 0));
            Assert.Throws<ArgumentException>(() => new Room(2, -1));
        }

        [Test]
        public void TestBookRoom()
        {
            Room room = new Room(2, 100);
            hotel.AddRoom(room);

            hotel.BookRoom(2, 0, 1, 100);

            Assert.AreEqual(1, hotel.Bookings.Count);
        }

        [Test]
        public void TestBookRoomShouldThrowExceptionIfAdultsIsSetToInvalidValue()
        {
            Room room = new Room(2, 100);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 0, 1, 100));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(-1, 0, 1, 100));
        }

        [Test]
        public void TestBookRoomShouldThrowExceptionIfChildrenIsSetToInvalidValue()
        {
            Room room = new Room(2, 100);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -1, 1, 100));
        }

        [Test]
        public void TestBookRoomShouldThrowExceptionIfResidenceDurationIsSetToInvalidValue()
        {
            Room room = new Room(2, 100);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 0, 0, 100));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 0, -1, 100));
        }

        [Test]
        public void BookRoom_NoBookingForNotEnoughBeds()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(3, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 200);

            Assert.That(hotel.Turnover.Equals(0));
        }

        [Test]
        public void BookRoom_WorksProperly()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(5, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 106);
            double expectedTurnover = 106;

            Assert.AreEqual(expectedTurnover, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(1));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }

        [Test]
        public void BookRoom_NoBookingIfTooLowBudget()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(5, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 100);
            double expectedTurnover = 0;

            Assert.AreEqual(expectedTurnover, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(0));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }
    }
}