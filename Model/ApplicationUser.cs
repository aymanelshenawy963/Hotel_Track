using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required,MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public EnumIsAdmin IsAdmin { get; set; }
        public byte[] ProfilePicture { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
