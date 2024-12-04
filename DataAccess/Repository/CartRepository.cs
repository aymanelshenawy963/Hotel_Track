using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{
    
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        private readonly ApplicationDbContext dbContext;
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
