using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
