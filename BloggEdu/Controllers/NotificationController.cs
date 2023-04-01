using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
