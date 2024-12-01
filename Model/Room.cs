namespace Model
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public double PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public Hotel Hotel { get; set; }

    }
}
