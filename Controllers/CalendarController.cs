using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using MyFinances.Database;
using Microsoft.EntityFrameworkCore;

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

            var model = new CalendarViewModel(currentYear, currentMonth, _dataService);

            return View(model);
        }
        [HttpGet]
        public IActionResult LoadTransactionModal(DateTime date, int type)
        {
            var model = new AddModalViewModel
            {
                TransactionType = type,
                HiddenDate = date,
                ExpenseCategories = _dataService.GetCategoriesByType(0),
                IncomeCategories = _dataService.GetCategoriesByType(1)
            };
            return PartialView("_TransactionModal", model);
        }
        [HttpPost]
        public IActionResult AddEntry(AddModalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(
                            x => x.Key,
                            x => x.Value.Errors.Select(e => e.ErrorMessage)
                            .ToList());

                return Json(new { success = false, errors = errors });
            }
            else
            {
                var transaction = new Transactions
                {
                    Date = model.HiddenDate,
                    Amount = model.Category == 4 ? (model.PayPerHour ?? 0) * (model.WorkedHours ?? 0) : (model.Amount ?? 0),
                    CategoriesID = model.Category,
                    UsersID = 1
                };

                _dataService.Add(transaction);
                return Ok(new { success = true });
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delete = _dataService.Delete(id);
            if (!delete) 
            { 
                return Json(new { success = false, message = "Element nie znaleziony" }); 
            }
            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult GetItemsByDate(DateTime date)
        {
            var items = _dataService.GetTransactions(date);

            return Json(items);
        }
    }

}