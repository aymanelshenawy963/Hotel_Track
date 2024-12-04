using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;
namespace DataAccess.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {

        private readonly ApplicationDbContext dbContext;
        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}