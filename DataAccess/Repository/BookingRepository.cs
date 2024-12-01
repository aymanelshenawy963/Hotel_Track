using DataAccess.Data;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;


namespace DataAccess.Repository
{
 

    public class BookingRepository : Repository<BookingRepository>, IBookingRepository
    {

        private readonly ApplicationDbContext dbContext;
        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
