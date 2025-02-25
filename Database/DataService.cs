

using MyFinances.Database;
using MyFinances.Models;

namespace WordZone.Services
{
    public class DataService
    {
        private readonly TransactionContext _context;

        public DataService(TransactionContext context)
        {
            _context = context;
        }

        public void Add(DateTime date,int type,double amount,string category)
        {
            /* only test if db conn works
            Transactions transaction = new Transactions
            {
                Amount = amount,
                Date = date,
                UsersID = 1,
                CategoriesID = 1,
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            */
        }
    }
}
