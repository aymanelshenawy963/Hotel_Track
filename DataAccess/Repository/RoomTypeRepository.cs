using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : Repository<RoomTypeRepository>, IRoomTypeRepository
    {
      
            private readonly ApplicationDbContext dbContext;
            public RoomTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                this.dbContext = dbContext;
            }
        
    }
}
