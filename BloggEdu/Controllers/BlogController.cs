using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
