using BusinessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.Controllers
{
    public class NewsLetterController : Controller
    {
        NewsLetterManager nm=new NewsLetterManager(new EFNewsLetterRepository());
        
        [HttpGet]
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SubscribeMail(NewsLetter p)
        {
            p.MailStatus = true;
            nm.AddNewsLetter(p);
            return PartialView();
        }
    }
}
