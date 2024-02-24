using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccsessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList;

namespace BloggEdu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page = 1)
        {
            var values = cm.GetList().ToPagedList(page, 9);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category p)
        {

            CategoryValidator cv = new CategoryValidator();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public IActionResult CategoryDelete(int id)
        {
            var value = cm.TGetById(id);
            cm.TDelete(value);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleCategoryStatus(int id)
        {
            var value = cm.TGetById(id);
            if (value != null)
            {
                value.CategoryStatus = !value.CategoryStatus; // Durumu tersine çevir
                cm.TUpdate(value);
                return Json(new { success = true, status = value.CategoryStatus });
            }
            return Json(new { success = false, message = "Kategori bulunamadı." });
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var value = cm.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            var existingCategory = cm.TGetById(category.CategoryID);
            if (ModelState.IsValid)
            {
                category.CategoryStatus = existingCategory.CategoryStatus;
                cm.TUpdate(category); 
                return RedirectToAction("Index"); 
            }
            return View(category); 
        }
    }
}
