using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Areas.Admin.Controllers
{
    public class WidgetController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
