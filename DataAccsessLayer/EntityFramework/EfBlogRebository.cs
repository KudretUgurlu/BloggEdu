﻿using DataAccsessLayer.Abstract;
using DataAccsessLayer.Concrete;
using DataAccsessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsessLayer.EntityFramework
{
    public class EfBlogRebository : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Where(x=>x.WriterID==id).ToList();
            }
        }
    }
}
