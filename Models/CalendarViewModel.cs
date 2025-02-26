
namespace MyFinances.Models
{
    public class CalendarViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DateTime> Days { get; set; } = new List<DateTime>();
        public DateTime hiddenDate { get; set; }
        public double Amount{ get; set; }
        public int TransactionType { get; set; }
        public int Category { get; set; }
        public List<Categories> ExpenseCategories { get; set; }
        public List<Categories> IncomeCategories { get; set; }

        public CalendarViewModel(int year, int month)
        {
            Year = year;
            Month = month;
            GenerateCalendar();
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
