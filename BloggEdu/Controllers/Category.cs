using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class Category : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
