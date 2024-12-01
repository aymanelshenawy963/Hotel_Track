using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    

    public class HotelRepository : Repository<HotelRepository>, IHotelRepository
    {

        private readonly ApplicationDbContext dbContext;
        public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
