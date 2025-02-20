using Microsoft.AspNetCore.Mvc;

namespace MyFinances.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
