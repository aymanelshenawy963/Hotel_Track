using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Booking
    {
        public int Id { get; set; }
        public bool CheckInData { get; set; }
        public bool CheckOutData { get; set; }
        public double TotalAmount { get; set; }
        public int PaymentId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string ApplicationUserId { get; set; }
        public Room Room { get; set; }
        public Payment Payment { get; set; }
        public Hotel Hotel { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
