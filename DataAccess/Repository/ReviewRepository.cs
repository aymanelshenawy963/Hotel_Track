using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{
 
        public class ReviewRepository : Repository<Review>, IReviewRepository
        {

            private readonly ApplicationDbContext dbContext;
            public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                this.dbContext = dbContext;
            }

        }
    
}
