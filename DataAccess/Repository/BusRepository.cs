using DataAccess.Repository.IRepository;
using DataAccess.Data;
using Model;


namespace DataAccess.Repository
{
    public class BusRepository : Repository<Bus>, IBusRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
