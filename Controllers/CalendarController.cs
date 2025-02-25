using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using WordZone.Services;

namespace MyFinances.Controllers
{
    public class CalendarController : Controller
    {
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
                DataService db = new DataService();
                db.Add(model.hiddenDate, model.TransactionType, model.Amount, model.Category);
            }

            return RedirectToAction("Calendar");
        }
    }
}

