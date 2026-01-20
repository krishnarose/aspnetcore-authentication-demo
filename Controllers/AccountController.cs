using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;

namespace AuthProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            User user = new User
            {
                Id = 1,
                Name = "krishna kumar",
                Role = "Admin",
                Created_at = DateTime.Now
            };

            ViewBag.userDetails = user;

            ViewData["pageTitle"] = "User Login Page";

            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
         public IActionResult CreateUser(string name, string role)
        {
            User user = new User
            {
                Id = 1,
                Name = name,
                Role = role,
                Created_at = DateTime.Now
            };
            return View("Register", user);
        }
    }
}
