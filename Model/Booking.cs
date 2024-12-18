using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime? CheckInDate { get; set; } //for hotel booking
        public DateTime? CheckOutDate { get; set; } //for hotel booking
        public double TotalAmount { get; set; }
        public int Count { get; set; }
        public enBookingType BookingType { get; set; } // hotel and transportation bookings
        public enBookingStatus Status { get; set; }

        // Hotel-specific fields
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Hotel Hotel { get; set; }


        // Transportation-specific fields
        public DateTime? TravelStartDate { get; set; }
        public DateTime? TravelEndDate { get; set; }
        public int? BusId { get; set; }
        public int? TrainId { get; set; }
        public int? CarId { get; set; }
        public Bus Bus { get; set; }
        public Train Train { get; set; }
        public Car Car { get; set; }
        
        public string ApplicationUserId { get; set; }

    }
}
