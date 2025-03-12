
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
        //Add transaction
        public void Add(Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
        }
        //Login User
        public Users Login(string login, string password)
        {
            return _context.Users.FirstOrDefault(u => (u.Login == login || u.Email == login) && u.Password == password)!;
        }
        //Check if Register new user is possible
        public int CheckRegister(string login, string email)
        {
            Console.WriteLine(_context.Users.Any(u => u.Login == login).ToString());
            if(_context.Users.Any(u => u.Email == email))
            {
                return 1;
            }
            if(_context.Users.Any(u=>u.Login == login))
            {
                return 2;
            }
            return 0;
        }
        //Register User
        public void Register(string login, string email,string password)
        {
            var user = new Users {Login = login ,Email = email, Password = password};
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        //Delete Transaction by ID
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
        //Get categories
        public List<Categories> GetCategories()
        {
            return _context.Categories.ToList();
        }
        //Get List of transaction for choosen day
        public List<Transactions> GetTransactions(DateTime date,int userID) 
        {
            return _context.Transactions.Include(item=> item.Category).Where(item=>item.Date == date && item.UsersID == userID).ToList();
        }
        //Get List of Category by type
        public List<Categories> GetCategoriesByType(int type)
        {
            return _context.Categories.Where(c => c.Type == type).ToList();
        }
        //Get List of Transactions by category in choosen date range
        public List<Transactions> GetTransactionsByCategory(int categoryID, int UserID, DateTime RangeStart, DateTime RangeEnd)
        {
            List<Transactions> transactions = new List<Transactions>();
            transactions = _context.Transactions.Where(item => item.CategoriesID == categoryID && item.UsersID == UserID && RangeStart<=item.Date && item.Date<=RangeEnd).OrderBy(item => item.Date).ToList();
            return transactions;
        }
        //Get List of transactions by type for choosen day
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
        //Get List of transactions amount by type in choosen date range
        public List<double> GetTransactionsAmountsByType(int UserID, DateTime RangeStart, DateTime RangeEnd, int type)
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
