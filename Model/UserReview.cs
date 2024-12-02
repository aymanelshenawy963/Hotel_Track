using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [PrimaryKey("ReviewId", "ApplicationUserId")]
    public class UserReview
    {
       

        public int ReviewId { get; set; }
        [ForeignKey(nameof(ReviewId))]
        [ValidateNever]
        public Review Reviews { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
      
    }
}
