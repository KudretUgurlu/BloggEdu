using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
