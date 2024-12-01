namespace Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public int HotelTypeId { get; set; }
        public EnumHotelType HotelType { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
