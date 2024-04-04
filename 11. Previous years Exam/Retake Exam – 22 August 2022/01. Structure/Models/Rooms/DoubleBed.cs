namespace BookingApp.Models.Rooms
{
    public class DoubleBed : Room
    {
        public const int BedCapacity = 2;

        public DoubleBed() : base(BedCapacity)
        {
        }
    }
}
