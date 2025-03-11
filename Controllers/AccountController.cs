using Microsoft.AspNetCore.Mvc;
using MyFinances.Database;
using System.Security.Cryptography;
using System.Text;

namespace MyFinances.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataService _dataService;
        public AccountController(DataService dataService)
        {
            _dataService = dataService;
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Calendar", "Calendar");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _dataService.Login(login, hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserID", user.ID);
                return RedirectToAction("Calendar", "Calendar");
            }

            ViewBag.Error = "Nieprawidłowy login lub hasło.";
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email,string login, string password, string passwordRepeat)
        {
            int a = _dataService.CheckRegister(login, email);
            if (a !=0)
            {
                switch (a)
                {
                    case 1:
                        ViewBag.Error = "Podany Email jest zajęty";
                        return View();
                    case 2:
                        ViewBag.Error = "Podany Login jest zajęty";
                        return View();
                }
            }
            if(passwordRepeat != password)
            {
                ViewBag.Error = "Hasła nie są takie same";
                return View();
            }
            var hashedPassword = HashPassword(password);
            _dataService.Register(login, email, hashedPassword);
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
