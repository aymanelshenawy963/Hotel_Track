using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{

    public class PaymentRepository : Repository<PaymentRepository>, IPaymentRepository
    {

        private readonly ApplicationDbContext dbContext;
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
