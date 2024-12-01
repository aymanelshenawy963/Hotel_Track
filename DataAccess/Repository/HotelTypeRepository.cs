using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    
    public class HotelTypeRepository : Repository<HotelTypeRepository>, IHotelTypeRepository
    {

        private readonly ApplicationDbContext dbContext;
        public HotelTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
