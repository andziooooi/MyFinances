

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

        public void Add(Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
        }

        public List<Categories> GetCategoriesByType(int type)
        {
            return _context.Categories.Where(c => c.Type == type).ToList();
        }

    }
}
