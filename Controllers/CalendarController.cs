using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using WordZone.Services;

namespace MyFinances.Controllers
{
    public class CalendarController : Controller
    {
        private readonly DataService _dataService; // 🔹 Dodaj pole na serwis

        public CalendarController(DataService dataService) // 🔹 Konstruktor z DI
        {
            _dataService = dataService;
        }
        public IActionResult Calendar(int? year, int? month)
        {
            int currentYear = year ?? DateTime.Now.Year;
            int currentMonth = month ?? DateTime.Now.Month;

            var model = new CalendarViewModel(currentYear, currentMonth);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddEntry(CalendarViewModel model)
        {
            if (model != null)
            {
                _dataService.Add(model.hiddenDate, model.TransactionType, model.Amount, model.Category);
            }

            return RedirectToAction("Calendar");
        }
    }
}

