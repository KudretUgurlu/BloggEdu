using BusinessLayer.Abstract;
using DataAccsessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        IAboutDal _aboudDal;

        public AboutManager(IAboutDal aboudDal)
        {
            _aboudDal = aboudDal;
        }

        public List<About> GetList()
        {
            return _aboudDal.GetListAll();
        }
    }
}
