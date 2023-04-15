using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
