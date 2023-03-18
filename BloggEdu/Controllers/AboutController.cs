using BusinessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
	public class AboutController : Controller
	{
		AboutManager abm=new AboutManager(new EfAboutRepository());
		public IActionResult Index()
		{
			return View();
		}
		public PartialViewResult SocialMediaAbout()
		{
			var values = abm.GetList();
			return PartialView(values);
		}
	}
}
