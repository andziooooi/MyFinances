
using Microsoft.EntityFrameworkCore;
using MyFinances.Models;

namespace MyFinances.Database
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
        public bool Delete(int ID) 
        {
            var TransToDelete= _context.Transactions.Find(ID);
            if (TransToDelete != null) 
            {
                _context.Transactions.Remove(TransToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Categories> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public List<Transactions> GetTransactions(DateTime date) 
        {
            return _context.Transactions.Where(item=>item.Date == date && item.UsersID == 1).ToList();
        }

        public List<Categories> GetCategoriesByType(int type)
        {
            return _context.Categories.Where(c => c.Type == type).ToList();
        }
        public List<Transactions> GetTransactionsByCategory(int categoryID, int UserID, DateTime RangeStart, DateTime RangeEnd)
        {
            List<Transactions> transactions = new List<Transactions>();
            transactions = _context.Transactions.Where(item => item.CategoriesID == categoryID && item.UsersID == UserID && RangeStart<=item.Date && item.Date<=RangeEnd).OrderBy(item => item.Date).ToList();
            return transactions;
        }
        public List<Transactions> GetDailyTransactionsByType(int UserID, DateTime Day,int type)
        {
            List<Transactions> transactions = new List<Transactions>();
            transactions = _context.Transactions
                .Include(item => item.Category)
                .Where(item => item.UsersID == UserID
                && Day==item.Date
                && item.Category.Type == type
                )
                .ToList();
            return transactions;
        }
        public List<double> GetMonthlyTransactionsAmountsByType(int UserID, DateTime RangeStart, DateTime RangeEnd, int type)
        {
            List<double> transactions = new List<double>();
            transactions = _context.Transactions
                .Include(item => item.Category)
                .Where(item => item.UsersID == UserID
                && RangeStart<=item.Date 
                && item.Date<=RangeEnd
                && item.Category.Type == type
                ).Select(item=>item.Amount)
                .ToList();
            return transactions;
        }
    }
}
