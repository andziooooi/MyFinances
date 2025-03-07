using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using MyFinances.Database;

namespace MyFinances.Controllers
{
    public class SummaryController : Controller
    {
        private readonly DataService _dataService;

        public SummaryController(DataService dataService)
        {
            _dataService = dataService;
        }
        public IActionResult Summary(int? type,int? year,int? month)
        {
            int selectedtype = type ?? 0;
            int currentYear = year ?? DateTime.Now.Year;
            int currentMonth = month ?? DateTime.Now.Month;
            SummaryViewModel model;
            if (selectedtype == 0)
            {
                model = new SummaryViewModel(selectedtype, currentYear, currentMonth, _dataService);
            }
            else
            {
                model = new SummaryViewModel(selectedtype, currentYear, _dataService);
            }

            return View(model);
        }
    }
}
