using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    
    public class UserReview
    {
        public int ReviewId { get; set; }
        [ValidateNever]
        public Review Review { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
      
    }
}
