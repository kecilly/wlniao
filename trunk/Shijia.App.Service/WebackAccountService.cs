using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.App.Domain;

namespace Shijia.App.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class WebackAccountService
        : GenericManagerBase<Domain.WebackAccount>, IWebackAccountService
    {
        public Shijia.App.Dao.IAccountDao AccountDao { set; get; }

        public IList<Domain.WebackAccount> LoadAllByPage(out long total, int page, int rows, string order, string sort)
        {
            return AccountDao.LoadAllByPage(out total, page, rows, order, sort).ToList();
        }
    }
}