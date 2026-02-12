using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
