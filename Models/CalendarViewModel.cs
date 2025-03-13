using MyFinances.Database;

namespace MyFinances.Models
{
    public class CalendarViewModel
    {
        private int UserID {  get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DateTime> Days { get; set; } = new List<DateTime>();
        public List<double> dayIncomeSum { get; set; } = new List<double>();
        public List<double> dayExpenseSum { get; set; } = new List<double>();
        public List<double> monthlyIncomeSum { get; set; } = new List<double>();
        public List<double> monthlyExpenseSum { get; set; } = new List<double>();

        public CalendarViewModel(int year, int month,DataService ds,int userID)
        {
            UserID = userID;
            Year = year;
            Month = month;
            GenerateCalendar();
            monthlyIncomeSum = ds.GetTransactionsAmountsByType(UserID, new DateTime(Year, Month, 1), new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month)),1);
            monthlyExpenseSum = ds.GetTransactionsAmountsByType(UserID, new DateTime(Year, Month, 1), new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month)), 0);
            foreach (DateTime day in Days)
            {
                var IncomeTrans = ds.GetDailyTransactionsByType(UserID, day.Date, 1);
                var OutcomeTrans = ds.GetDailyTransactionsByType(UserID, day.Date, 0);
                double DailyIncomeSum = 0;
                double DailyOutcomeSum = 0;
                if(IncomeTrans !=null)
                {
                    foreach (var t in IncomeTrans)
                    {
                        DailyIncomeSum += t.Amount;
                    }
                    dayIncomeSum.Add(DailyIncomeSum);
                }
                else
                {
                    dayIncomeSum.Add(0);
                }
                if(OutcomeTrans !=null)
                {
                    foreach(var t in OutcomeTrans)
                    {
                        DailyOutcomeSum += t.Amount;
                    }
                    dayExpenseSum.Add(DailyOutcomeSum);
                }
                else
                {
                    dayExpenseSum.Add(0);
                }
            }
           
        }
        public CalendarViewModel() { }

        private void GenerateCalendar()
        {
            Days.Clear();
            DateTime firstDay = new DateTime(Year, Month, 1); 
            
            int daysInMonth = DateTime.DaysInMonth(Year, Month); 

            int firstDayOfWeek = (int)firstDay.DayOfWeek -1; 

            DateTime startDate = firstDay.AddDays(-firstDayOfWeek);

            for (int i = 0; i < 42; i++)
            {
                Days.Add(startDate.AddDays(i));
            }
        }
        public object GetPreviousMonthParams(int currentYear, int currentMonth)
        {
            if (currentMonth == 1)
            {
                return new { year = currentYear - 1, month = 12 };
            }
            else
            {
                return new { year = currentYear, month = currentMonth - 1 }; 
            }
        }
        public object GetNextMonthParams(int currentYear, int currentMonth)
        {
            if (currentMonth == 12)
            {
                return new { year = currentYear + 1, month = 1 }; 
            }
            else
            {
                return new { year = currentYear, month = currentMonth +1 };
            }
        }
    }
}
