using BusinessLayer.Concrete;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BloggEdu.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRebository());
        public IActionResult Index()
        {
            Context c= new Context();
            ViewBag.v1=c.Blogs.Count().ToString();
            ViewBag.v2=c.Blogs.Where(x=>x.WriterID==1).Count();
            ViewBag.v3=c.Categories.Count().ToString();
            return View();
        }
    }
}
