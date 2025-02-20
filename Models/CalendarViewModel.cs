using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyFinances.Models
{
    public class CalendarViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DateTime> Days { get; set; } = new List<DateTime>();

        public CalendarViewModel(int year, int month)
        {
            Year = year;
            Month = month;

            GenerateCalendar();
        }

        private void GenerateCalendar()
        {
            Days.Clear();
            DateTime firstDay = new DateTime(Year, Month, 1); 
            
            int daysInMonth = DateTime.DaysInMonth(Year, Month); 

            int firstDayOfWeek = (int)firstDay.DayOfWeek -1; 

            // Znalezienie pierwszego dnia w siatce kalendarza
            DateTime startDate = firstDay.AddDays(-firstDayOfWeek);

            // Tworzenie siatki kalendarza (6 tygodni x 7 dni = 42)
            for (int i = 0; i < 42; i++)
            {
                Days.Add(startDate.AddDays(i));
            }
        }
    }
}
