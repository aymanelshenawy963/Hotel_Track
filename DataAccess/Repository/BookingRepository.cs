using DataAccess.Data;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Model;


namespace DataAccess.Repository
{
 

    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly ApplicationDbContext dbContext;
        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
