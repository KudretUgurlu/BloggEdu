using DataAccsessLayer.Abstract;
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
    public class EfMessage2Repository : GenericRepository<Message2>, IMessage2Dal
    {
        public List<Message2> GetSendBoxWithMessageByWriter(int id)
        {
            using (var c=new Context())
            {
                return c.Message2s.Include(x=>x.ReceiverUser).Where(y=>y.SenderID == id).ToList();
            }
        }

        public List<Message2> GetInboxWithMessageByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Message2s.Include(x => x.SenderUser).Where(x => x.ReceiverID == id).ToList();
            }
        }

    }
}
