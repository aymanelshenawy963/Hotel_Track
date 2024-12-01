namespace Model
{
    public class HotelType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumHotelType Type { get; set; }
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}
