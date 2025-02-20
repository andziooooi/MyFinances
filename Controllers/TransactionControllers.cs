using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using System;

namespace MyFinances.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index(int? year, int? month)
        {
            int currentYear = year ?? DateTime.Now.Year;
            int currentMonth = month ?? DateTime.Now.Month;

            var model = new CalendarViewModel(currentYear, currentMonth);
            return View(model);
        }
    }
}

