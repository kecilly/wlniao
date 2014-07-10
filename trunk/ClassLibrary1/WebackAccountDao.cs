using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;

namespace Shijia.App.Dao
{
   [Spring.Stereotype.Repository]
    public class AccountDao
        : RepositoryBase<Domain.WebackAccount>, IAccountDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }

        public IQueryable<Domain.WebackAccount> LoadAllByPage(out long total, int page, int rows, string order, string sort)
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

        public Domain.WebackAccount Get(string AppUserId)
        {
            return this.LoadAll().FirstOrDefault(f => f.AppUserId == AppUserId);
        }
    }
}
