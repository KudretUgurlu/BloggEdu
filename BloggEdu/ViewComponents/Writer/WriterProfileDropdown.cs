using BusinessLayer.Concrete;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BloggEdu.ViewComponents.Writer
{
    public class WriterProfileDropdown : ViewComponent
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = wm.GetWriterById(writerID);
            var usernamesurname = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            var writerimage = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterImage).FirstOrDefault();
            ViewBag.usernamesurname = usernamesurname;
            ViewBag.username = username;
            ViewBag.writerimage = writerimage;
            return View(values);
        }
    }
}
