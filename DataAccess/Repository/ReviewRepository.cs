using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
 
        public class ReviewRepository : Repository<ReviewRepository>, IReviewRepository
        {

            private readonly ApplicationDbContext dbContext;
            public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                this.dbContext = dbContext;
            }

        }
    
}
