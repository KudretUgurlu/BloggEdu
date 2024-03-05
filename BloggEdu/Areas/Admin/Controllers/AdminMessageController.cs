using BusinessLayer.Concrete;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BloggEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        Context c = new Context();
        public IActionResult Inbox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerID);
            var Inboxcount = mm.GetInboxListByWriter(writerID).Count();
            ViewBag.Inboxcount = Inboxcount;
            var Senboxcount = mm.GetSendBoxListByWriter(writerID).Count();
            ViewBag.Senboxcount = Senboxcount;
            return View(values);
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerID);
            var Inboxcount = mm.GetInboxListByWriter(writerID).Count();
            ViewBag.Inboxcount = Inboxcount;
            var Senboxcount = mm.GetSendBoxListByWriter(writerID).Count();
            ViewBag.Senboxcount = Senboxcount;
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ComposeMessage(Message2 model, string ReceiverEmail)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var senderID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            var receiverID = c.Users.Where(x => x.Email == ReceiverEmail).Select(y => y.Id).FirstOrDefault();
            if (receiverID == 0)
            {
                ViewBag.ErrorMessage = "Alıcı bulunamadı.";
                return View(model);
            }
            model.SenderID = senderID;
            model.ReceiverID = receiverID;
            model.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            model.MessageStatus = true;
            mm.TAdd(model);

            return RedirectToAction("SendBox");
        }

    }
}
