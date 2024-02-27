using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
        public string UserAbout {  get; set; }
        public string UserTitle {  get; set; }

        [ForeignKey("Writer")]
        public int? WriterID { get; set; }
        public Writer Writer { get; set; }

        public virtual ICollection<Message2> WriterSender { get; set; }
        public virtual ICollection<Message2> WriterReceiver { get; set; }

       
        public virtual ICollection<Blog> Blogs { get; set; }


    }
}
