using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




namespace BloggEdu.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRebository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        Context c = new Context();
        
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }
        //[AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogByID(id);
            var writerID = c.Blogs.Where(x=>x.BlogID==id).Select(y=>y.WriterID).FirstOrDefault();
            var writername=c.Writers.Where(x=>x.WriterID==writerID).Select(y=>y.WriterName).FirstOrDefault();
            ViewBag.writername = writername;
            return View(values);
        }
        public IActionResult BlogListByWriter()
        {
            
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = bm.GetLİstWithCategoryByWriterBm(writerID);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {

            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            ViewData["CategoryID"] = categoryvalues;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog p, IFormFile BlogImage)
        {
           
            var username = User.Identity.Name;
            var userId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();



            if (BlogImage != null && BlogImage.Length > 0)
            {
                // Dosya adını alın
                string fileName = Path.GetFileName(BlogImage.FileName);

                // Belirlediğiniz bir klasöre kaydedin (örneğin wwwroot/images)
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BlogImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BlogImage.CopyTo(stream);
                }

                // Blog nesnesinin BlogImage özelliğine dosya yolunu kaydedin
                p.BlogImage = "/images/BlogImages/" + fileName;
            }

            BlogValidator bv = new BlogValidator();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID = userId;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            ViewData["CategoryID"] = new SelectList(cm.GetList(), "CategoryID", "CategoryName");
            return View();

        }
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p, IFormFile BlogImage)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            p.WriterID = writerID;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            if (BlogImage != null && BlogImage.Length > 0)
            {
                // Dosya adını alın
                string fileName = Path.GetFileName(BlogImage.FileName);

                // Belirlediğiniz bir klasöre kaydedin (örneğin wwwroot/images)
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/BlogImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BlogImage.CopyTo(stream);
                }

                // Blog nesnesinin BlogImage özelliğine dosya yolunu kaydedin
                p.BlogImage = "/images/BlogImages/" + fileName;
            }
            else
            {
                // BlogImage yüklenmediğinde, mevcut görseli koruyun
                // Önceki blog görselini almak için mevcut blog nesnesini veritabanından sorgulayın
                var existingBlog = bm.TGetById(p.BlogID);
                if (existingBlog != null)
                {
                    p.BlogImage = existingBlog.BlogImage; // Mevcut görseli kullan
                }
            }
                bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
        public IActionResult ChangeBlogStatus(int id)
        {
            // Blog id'sine göre veritabanından ilgili blogu getirin
            var blog = bm.TGetById(id);

            if (blog != null)
            {
                // Blogun durumunu tersine çevirin
                blog.BlogStatus = !blog.BlogStatus;

                // Blogun güncel halini veritabanına kaydedin
                bm.TUpdate(blog);
            }

            // BlogListByWriter sayfasına geri dönün
            return RedirectToAction("BlogListByWriter");
        }
    }
}
