using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;

using AuthProject.Data;

using System.Threading.Tasks;
using AuthProject.Models.ViewModels;
using Dapper;

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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(model);
            }

            using (var connection = _context.CreateConnection())
            {
                string query = "SELECT * FROM users WHERE name = @Username ORDER BY id ASC LIMIT 1";

                var parameter = new DynamicParameters();
                parameter.Add("Username", model.Username);

                var user = await connection.QueryFirstOrDefaultAsync<User>(query, parameter);

                if(user == null)
                {
                    ModelState.AddModelError("", "Invalid credentials.");
                    return View(model);
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.password);
                if (!isPasswordValid)
                {
                    ModelState.AddModelError("", "Invalid credentials.");
                    return View(model);
                }
                // Authentication successful, redirect to dashboard or home page
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
        }

       

        public IActionResult Register()
        {
           
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
           if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Please correct the errors and try again.");
                return View(model);
            }

            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password); // Implement password encryption here
            
            using (var connection = _context.CreateConnection())
            {
                string insertQuery = "INSERT INTO users (name, role, password, created_at) VALUES (@Name, @Role, @Password, @CreatedAt)";
                var parameters = new DynamicParameters();
                parameters.Add("Name", model.Name);
                parameters.Add("Role", model.Role);
                parameters.Add("Password", encryptedPassword);
                parameters.Add("CreatedAt", DateTime.Now);
                var executed = await connection.ExecuteAsync(insertQuery, parameters);

                if (executed > 0)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            return View(model);
        }
    }
}
