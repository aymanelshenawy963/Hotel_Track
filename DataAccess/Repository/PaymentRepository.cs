using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;

namespace DataAccess.Repository
{

    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {

        private readonly ApplicationDbContext dbContext;
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
