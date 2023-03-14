using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
