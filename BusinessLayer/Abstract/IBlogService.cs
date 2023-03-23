using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    internal interface IBlogService:IGenericService<Blog>
    {
        List<Blog> GetBlogListWithCategory();
       
        List<Blog> GetBlogListByWriter(int id);
    }
}
