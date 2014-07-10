using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;

namespace Shijia.Dao
{
   [Spring.Stereotype.Repository]
    public class MPUserInfoDao
        : RepositoryBase<Domain.MPUserInfo>, IMPUserInfoDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }

        public IQueryable<Domain.MPUserInfo> LoadAllByPage(int accountID,out long total, int page, int rows, string order, string sort)
        {
            var list = from li in this.LoadAll()
                       where li.AccountId == accountID
                       select li;

            total = list.LongCount();

            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sort))
            {
                list = list.OrderBy(sort + " " + order);
            }
            list = list.Skip((page - 1) * rows).Take(rows);

            return list;
        }

        public Domain.MPUserInfo Get(String OpenId, int AccountId = 0)
        {
            return this.LoadAll().FirstOrDefault(f => f.AccountId == AccountId && f.OpenId == OpenId);           
        }

        public Domain.MPUserInfo GetByMobileNumber(String MobileNumber, int AccountId = 0)
        {
            return this.LoadAll().FirstOrDefault(f => f.AccountId == AccountId && f.MobileNumber == MobileNumber);
        }
    }
}
