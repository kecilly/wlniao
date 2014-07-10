using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;

namespace Shijia.Dao
{
   [Spring.Stereotype.Repository]
    public class NoticeDao
        : RepositoryBase<Domain.Notice>, INoticeDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }

        public IQueryable<Domain.Notice> LoadAllByPage(out long total, int page, int rows, string order, string sort)
        {
            var list = this.LoadAll();

            total = list.LongCount();

            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sort))
            {
                list = list.OrderBy(sort + " " + order);
            }
            list = list.Skip((page - 1) * rows).Take(rows);

            return list;
        }

        public IQueryable<Domain.Notice> LoadALL(int accountID)
        {//"RangeId=1 or RangeId=0 order by Id desc
            var list = from li in this.LoadAll()
                       where (li.AccountID == accountID &&  li.RangeId==2) || (li.RangeId==0 || li.RangeId==1)
                       select li;
            list = list.OrderBy("Id desc");
            return list;
        }
    }
}
