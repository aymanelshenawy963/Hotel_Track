using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{
    

    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {

        private readonly ApplicationDbContext dbContext;
        public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
