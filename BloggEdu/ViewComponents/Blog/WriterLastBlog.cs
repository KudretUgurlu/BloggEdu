using BusinessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.ViewComponents.Blog
{
	public class WriterLastBlog:ViewComponent
	{
		BlogManager bm = new BlogManager( new EfBlogRebository());

		public IViewComponentResult Invoke()
		{
			var values = bm.GetBlogListByWriter(1);
			return View(values);
		}
	}
}
