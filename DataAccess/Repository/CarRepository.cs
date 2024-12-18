using DataAccess.Data;
using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{
    public class CarRepository: Repository<Car> , ICarRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }   
    }
}
