using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Linq;

namespace Shijia.Dao
{
   [Spring.Stereotype.Repository]
    public class HandleRuleDao
        : RepositoryBase<Domain.HandleRule>, IHandleRuleDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }

        public IQueryable<Domain.HandleRule> LoadAllByPage(int accountID,out long total, int page, int rows, string order, string sort)
        {
            var list = from li in this.LoadAll()
                       where li.GetMode == 2 && li.AccountId == accountID
                       select li;

            total = list.LongCount();

            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sort))
            {
                list = list.OrderBy(sort + " " + order);
            }
            list = list.Skip((page - 1) * rows).Take(rows);

            return list;
        }

        public IQueryable<Domain.HandleRule> LoadAllByPage(int accountID, int level, out long total, int page, int rows, string order, string sort)
        {
            var list = from li in this.LoadAll()
                       where li.RuleLevel == level && li.AccountId == accountID
                       select li;

            total = list.LongCount();

            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sort))
            {
                list = list.OrderBy(sort + " " + order);
            }
            list = list.Skip((page - 1) * rows).Take(rows);

            return list;
        }

        public IQueryable<Domain.HandleRule> LoadAllByAccountID(int accountID)
        {
           
            return from li in this.LoadAll()
                   where li.GetMode==1 && li.AccountId==accountID                 
                   select li;
           
        }

        public IQueryable<Domain.HandleRule> LoadAllByKeyWord(int accountID, string keyWord)
        {
            return from li in this.LoadAll()
                   where li.KeyWord == keyWord && li.AccountId == accountID
                   orderby li.KeyWord ascending
                   select li;
        }
    }
}
