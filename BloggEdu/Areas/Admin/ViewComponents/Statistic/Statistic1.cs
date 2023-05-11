using BusinessLayer.Concrete;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace BloggEdu.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRebository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = bm.GetList().Count();
            ViewBag.v2=c.Contacts.Count();
            ViewBag.v3=c.Comments.Count();

            string api = "f78fb14cfbd5388a8af5227b7d1b01ca";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            ViewBag.v4=document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
