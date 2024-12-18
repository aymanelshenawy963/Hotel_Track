using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [PrimaryKey("BookingId", "ApplicationUserId")]
    public class Cart
    {
        internal string? userManager;

        public int BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        [ValidateNever]
        public Booking Booking { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
