
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Reflection.Emit;

namespace DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings {  get; set; }
        public DbSet<Hotel> Hotels {  get; set; }
        public DbSet<Room> Rooms {  get; set; }
        public DbSet<Review> Reviews {  get; set; }
        public DbSet<Cart> Carts {  get; set; }
        public DbSet<UserReview> UserReviews {  get; set; }
        public DbSet<Car> Cars {  get; set; }
        public DbSet<Bus> Buses {  get; set; }
        public DbSet<Train> Trains {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //base.OnModelCreating(builder);
            
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            // تحديد العلاقات الأخرى
            modelBuilder.Entity<UserReview>()
            .HasKey(sc => new { sc.ReviewId, sc.ApplicationUserId });
           
            modelBuilder.Entity<UserReview>()
            .HasOne(ur => ur.Review)
            .WithMany(r => r.UserReviews) // Assuming Review can have many UserReviews
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserReview>()
                .HasOne(ur => ur.ApplicationUser)
                .WithMany(a => a.UserReviews) // Assuming ApplicationUser can have many UserReviews
                .OnDelete(DeleteBehavior.Restrict);

            // Disable cascading deletes globally if needed (example)

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);  // أو حسب الحاجة
                                                     // تحديد العلاقات الأخرى

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany()
                .HasForeignKey(b => b.CarId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Bus)
                .WithMany()
                .HasForeignKey(b => b.BusId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Train)
                .WithMany()
                .HasForeignKey(b => b.TrainId);
        }



    }

}
