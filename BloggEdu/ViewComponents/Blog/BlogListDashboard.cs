using BusinessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BloggEdu.ViewComponents.Blog
{
    public class BlogListDashboard:ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRebository());

        public IViewComponentResult Invoke()
        {
            //var values = bm.GetBlogListWithCategory();
            var values = bm.GetBlogListWithCategory().OrderByDescending(x => x.BlogID).Take(10).ToList();
            return View(values);
        }
    }
}
