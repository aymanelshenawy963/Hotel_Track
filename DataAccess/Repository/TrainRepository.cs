using DataAccess.Data;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{
    public class TrainRepository : Repository<Train>, ITrainRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TrainRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
