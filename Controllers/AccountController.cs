using Microsoft.AspNetCore.Mvc;
using AuthProject.Models;

using AuthProject.Data;

using System.Threading.Tasks;
using AuthProject.Models.ViewModels;
using Dapper;
using AuthProject.Models.ServiceModel;
using System.Text.Json;
using AuthProject.Services;
using System.Collections;

namespace AuthProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly DapperContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthContextService _authContextService;

        public AccountController(
            DapperContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuthContextService authContextService

            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authContextService = authContextService;
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
                string query = "SELECT * FROM users WHERE username = @Username ORDER BY id ASC LIMIT 1";

                var parameter = new DynamicParameters();
                parameter.Add("Username", model.Username);

                var user = await connection.QueryFirstOrDefaultAsync<User>(query, parameter);

                if (user == null)
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
                // creating session or cookie can be done here

                var isSessionSet = _authContextService.SetSession(user);
                if (!isSessionSet)
                {
                    ModelState.AddModelError("", "Failed to create user session. Please try again.");
                    return View(model);
                }

                // TempData["SuccessMessage"] = "Login successful!";
                // return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Login successful.");
                return View(model);
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
                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(model);
            }

            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password); // Implement password encryption here

            using (var connection = _context.CreateConnection())
            {
                string insertQuery = "INSERT INTO users (name, role, password, created_at, created_by, created_ip,mobile,username) VALUES (@Name, @Role, @Password, @CreatedAt, @CreatedBy, @CreatedIp,@Mobile,@Username)";
                var parameters = new DynamicParameters();
                parameters.Add("Name", model.Name);
                parameters.Add("Role", model.Role);
                parameters.Add("Password", encryptedPassword);
                parameters.Add("CreatedAt", DateTime.Now);
                parameters.Add("CreatedBy", -1); // Assuming default user ID
                parameters.Add("CreatedIp", _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown"); // Assuming default IP address
                parameters.Add("Mobile", model.Mobile);
                parameters.Add("Username", model.Username);

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
