using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinances.Models;
using WordZone.Services;

namespace MyFinances.Controllers
{
    public class CalendarController : Controller
    {
        private readonly DataService _dataService;

        public CalendarController(DataService dataService) 
        {
            _dataService = dataService;
        }
        public IActionResult Calendar(int? year, int? month)
        {
            int currentYear = year ?? DateTime.Now.Year;
            int currentMonth = month ?? DateTime.Now.Month;

            var model = new CalendarViewModel(currentYear, currentMonth);
            model.ExpenseCategories = _dataService.GetCategoriesByType(0);
            model.IncomeCategories = _dataService.GetCategoriesByType(1);

            return View(model);
        }
        [HttpPost]
        public IActionResult AddEntry(CalendarViewModel model)
        {
            if (model != null)
            {
                Transactions transaction = new Transactions
                {
                    Date = model.hiddenDate,
                    Amount = model.Amount,
                    CategoriesID = model.Category,
                    UsersID = 1
                };
                _dataService.Add(transaction);
            }

            return RedirectToAction("Calendar");
        }
    }
}

