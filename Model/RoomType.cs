namespace Model
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumHotelType Type { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
