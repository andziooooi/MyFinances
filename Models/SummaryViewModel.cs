using MyFinances.Database;

namespace MyFinances.Models
{
    public class SummaryViewModel
    {
        public int Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime SummaryRangeStart { get; set; }
        public DateTime SummaryRangeEnd { get; set; }
        public List<Categories> Categories { get; set; }
        public double IncomeSum { get; set; } 
        public double ExpenseSum { get; set; }
        public List<double> IncomeList { get; set; } = new List<double>();
        public List<double> ExpenseList { get; set; } = new List<double>();
        public List<string> Labels { get; set; }
        public int LabelPointer {  get; set; }


        private int UserID {  get; set; }
        public SummaryViewModel(int type,int year, int month,DataService ds,int userID)
        {
            UserID = userID;
            Type = type;
            Year = year;
            Month = month;
            LabelPointer = Month;
            SummaryRangeStart = new DateTime(Year, Month, 1);
            SummaryRangeEnd = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            Labels= new List<string> { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };

            Categories = ds.GetCategories();
            foreach (var c in Categories)
            {
                c.Transactions = ds.GetTransactionsByCategory(c.ID, UserID, SummaryRangeStart, SummaryRangeEnd);
                c.Sum = SumTransactions(c.Transactions);
                if (c.Type == 1)
                {
                    IncomeSum += c.Sum;
                }
                else
                {
                    ExpenseSum += c.Sum;
                }
            }
            for (int i = 1; i<13; i++)
            {
                var incomeList = ds.GetTransactionsAmountsByType(userID, new DateTime(year, i, 1), new DateTime(Year, i, DateTime.DaysInMonth(Year, i)), 1);
                IncomeList.Add(incomeList.Sum());
                var expenseList = ds.GetTransactionsAmountsByType(userID, new DateTime(year, i, 1), new DateTime(Year, i, DateTime.DaysInMonth(Year, i)), 0);
                ExpenseList.Add(expenseList.Sum());
            }
        }
        public SummaryViewModel(int type, int year, DataService ds,int userID)
        {
            UserID = userID;
            Type = type;
            Year = year;
            SummaryRangeStart = new DateTime(Year, 1, 1);
            SummaryRangeEnd = new DateTime(Year, 12, 31);
            if (Year == DateTime.Now.Year)
            {
                Labels= new List<string> { (Year-2).ToString(), (Year-1).ToString(), Year.ToString() };
                for (int i = Year-2; i <Year+1; i++)
                {
                    var incomeList = ds.GetTransactionsAmountsByType(userID, new DateTime(i, 1, 1), new DateTime(i, 12, 31),1);
                    IncomeList.Add(incomeList.Sum());
                    var expenseList = ds.GetTransactionsAmountsByType(userID, new DateTime(i, 1, 1), new DateTime(i, 12, 31), 0);
                    ExpenseList.Add(expenseList.Sum());
                }
            }
            else
            {
                Labels= new List<string> { (Year-1).ToString(), Year.ToString(), (Year+1).ToString() };
                for (int i = Year-1; i < Year+2; i++)
                {
                    var incomeList = ds.GetTransactionsAmountsByType(userID, new DateTime(i, 1, 1), new DateTime(i, 12, 31), 1);
                    IncomeList.Add(incomeList.Sum());
                    var expenseList = ds.GetTransactionsAmountsByType(userID, new DateTime(i, 1, 1), new DateTime(i, 12, 31), 0);
                    ExpenseList.Add(expenseList.Sum());
                }
            }
            LabelPointer = Year;
            Categories = ds.GetCategories();
            foreach (var c in Categories)
            {
                c.Transactions = ds.GetTransactionsByCategory(c.ID, UserID, SummaryRangeStart, SummaryRangeEnd);
                c.Sum = SumTransactions(c.Transactions);
                if (c.Type == 1)
                {
                    IncomeSum += c.Sum;
                }
                else
                {
                    ExpenseSum += c.Sum;
                }
            }
        }
        public object GetPreviousMonthParams(int currentYear, int currentMonth)
        {
            if (currentMonth == 1)
            {
                return new {type = 0, year = currentYear - 1, month = 12 };
            }
            else
            {
                return new {type =0, year = currentYear, month = currentMonth - 1 };
            }
        }
        public object GetNextMonthParams(int currentYear, int currentMonth)
        {
            if (currentMonth == 12)
            {
                return new {type=0, year = currentYear + 1, month = 1 };
            }
            else
            {
                return new {type=0, year = currentYear, month = currentMonth +1 };
            }
        }
        private double SumTransactions(List<Transactions> transactions)
        {
            double sum = 0;
            foreach (var trans in transactions)
            {
                sum += trans.Amount;
            }
            return sum;
        }
    }

}
