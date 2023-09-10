using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BloggEdu.Models
{
    public class UserUpdateViewModel
    {
        public string namesurname { get; set; }
        public string username { get; set; }
        public string mail { get; set; }
        public string imageurl { get; set; }

        [Display(Name = "Profil Resmi")]
        public IFormFile ImageFile { get; set; }

        public string password { get; set; }
        public string confirmPassword { get; set; }
        public bool ChangePassword { get; set; }
    }
}
