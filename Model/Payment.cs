using Stripe;

namespace Model
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public EnumPay PaymentMethod { get; set; }
        public DateTime PaymentData { get; set; }
        public Booking Booking { get; set; }

    }
}
