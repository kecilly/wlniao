using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;

namespace Shijia.Dao
{
   [Spring.Stereotype.Repository]
    public class MPMenuDao
        : RepositoryBase<Domain.MPMenu>, IMPMenuDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }

        public IQueryable<Domain.MPMenu> LoadAllByPage(out long total, int page, int rows, string order, string sort)
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

        public IQueryable<Domain.MPMenu> GetByAccount(int accountID)
        {
            var list = from li in this.LoadAll()
                       where (li.AccountId == accountID)
                       select li;
            return list;
        }
    }
}
