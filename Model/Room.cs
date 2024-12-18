namespace Model
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public double PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int AdultCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public EnumRoomType RoomType { get; set; }
        public int HotelId { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public Hotel Hotel { get; set; }

    }
}
