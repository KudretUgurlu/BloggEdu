using BusinessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.ViewComponents.Notification
{
    public class NotificationList:ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRebository());

        public IViewComponentResult Invoke()
        {
            var values = bm.GetLast3Blog();
            return View(values);
        }
    }
}
