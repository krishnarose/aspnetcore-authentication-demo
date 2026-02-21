using AuthProject.Data;
using AuthProject.Models.DTOs;
using AuthProject.Models.ViewModels;
using AuthProject.Repositories;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthProject.Controllers
{

    public class EnquiryController : Controller
    {
        private readonly DapperContext _context;
        private readonly IGenericRepository _genericRepository;

        public EnquiryController(DapperContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(EnquiryVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(model);
            }

            using (var connection = _context.CreateConnection())
            {
                string insertQuery = "INSERT INTO enquiries (first_name, last_name, email, phone, description) VALUES (@First_Name, @Last_Name, @Email, @Phone, @Description)";
                var parameters = new DynamicParameters();
                parameters.Add("first_name", model.First_Name);
                parameters.Add("last_name", model.Last_Name);
                parameters.Add("email", model.Email);
                parameters.Add("phone", model.Phone);
                parameters.Add("description", model.Description);

                var executes = await connection.ExecuteAsync(insertQuery, parameters);
                if (executes > 0)
                {
                    TempData["SuccessMessage"] = "Your enquiry has been submitted successfully!";
                    return RedirectToAction(nameof(ViewAllEnquiry));
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while submitting your enquiry. Please try again.");
                }

                return View(model);
            }

        }

 

        [HttpGet]

        public async Task<IActionResult> ViewAllEnquiry()
        {

            var enquiries = await _genericRepository.GetAsync<EnquiryVM>(options: new QueryOptions
            {
                SelectColumns = new List<string> { "id", "first_name",  "last_name", "email", "phone", "description" },
                Table = "enquiries",
               
            });
            return View(enquiries);
            
        }


    }
}
