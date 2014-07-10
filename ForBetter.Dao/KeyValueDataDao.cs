using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;

namespace Shijia.Dao
{
   [Spring.Stereotype.Repository]
    public class KeyValueDataDao
        : RepositoryBase<Domain.KeyValueData>, IKeyValueDataDao, INHibernateSessionFactory
    {
        // Nhibernate 会话支持
        public NHibernate.ISessionFactory SessionFactory { set; get; }
        public IQueryable<Domain.KeyValueData> LoadAllByPage(out long total, int page, int rows, string order, string sort)
        {
            var list = this.LoadAll();

            total = list.LongCount();

            list = list.OrderBy(sort + " " + order);
            list = list.Skip((page - 1) * rows).Take(rows);

            return list;
        }

        public Domain.KeyValueData GetByKeyName(string KeyName)
        {
            return this.LoadAll().FirstOrDefault(f => f.KeyName == KeyName );
        }
    }
}
