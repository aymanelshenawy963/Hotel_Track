namespace Model
{
    public class Review
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public DateTime Data { get; set; }
        public string Comment { get; set; }
        public string ApplicationUserId { get; set; }
        public int HotelId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
        public ICollection<UserReview> UserReviews { get; set; }
        public Hotel Hotel { get; set; }
    }
}
