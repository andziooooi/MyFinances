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
        public SummaryViewModel(int type,int year, int month,DataService ds)
        {
            Type = type;
            Year = year;
            Month = month;
            SummaryRangeStart = new DateTime(Year, Month, 1);
            SummaryRangeEnd = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

            Categories = ds.GetCategories();
            foreach (var c in Categories)
            {
                c.Transactions = ds.GetTransactionsByCategory(c.ID, 1, SummaryRangeStart, SummaryRangeEnd);
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
        public SummaryViewModel(int type, int year, DataService ds)
        {
            Type = type;
            Year = year;
            SummaryRangeStart = new DateTime(Year, 1, 1);
            SummaryRangeEnd = new DateTime(Year, 12, 31);

            Categories = ds.GetCategories();
            foreach (var c in Categories)
            {
                c.Transactions = ds.GetTransactionsByCategory(c.ID, 1, SummaryRangeStart, SummaryRangeEnd);
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
