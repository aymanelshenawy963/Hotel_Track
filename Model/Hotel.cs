using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        public string HotelType { get; set; }
        [ValidateNever]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        [ValidateNever]
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [ValidateNever]
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
