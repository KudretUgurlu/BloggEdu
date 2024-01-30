using BloggEdu.Models;
using BusinessLayer.Concrete;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BloggEdu.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterUserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                // AppUser oluştur
                var user = new AppUser()
                {
                    Email = p.Mail,
                    UserName = p.UserName,
                    NameSurname = p.NameSurname
                };

                // Kullanıcıyı kaydet
                var result = await _userManager.CreateAsync(user, p.Password);

                if (result.Succeeded)
                {
                    // Writer oluştur
                    var writer = new Writer()
                    {
                        WriterName = p.UserName,
                        WriterMail = p.Mail,
                        WriterPassword = p.Password,
                        WriterStatus = true // veya istediğiniz bir değer
                    };

                    using (var context = new Context()) 
                    {
                        context.Writers.Add(writer); // Writer'ı ekleyin
                        context.SaveChanges(); // Değişiklikleri kaydedin
                    }
                    // Writer ve AppUser ilişkisini tanımla
                    writer.AppUser = user;

                    // Veritabanına Writer'ı kaydet
                    // DbContext aracılığıyla kaydetme işlemi burada gerçekleşir

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(p);
        }
    }
}
