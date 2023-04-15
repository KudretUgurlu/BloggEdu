using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    
    public class AdminController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
