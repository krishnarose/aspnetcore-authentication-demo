using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;
using AuthProject.Data;

namespace AuthProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly DapperContext _context;

        public AccountController(DapperContext context)
        {
            _context = context;
        }
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
            var connection = _context.CreateConnection();
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
