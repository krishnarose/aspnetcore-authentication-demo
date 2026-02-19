using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    public class EnquiryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
