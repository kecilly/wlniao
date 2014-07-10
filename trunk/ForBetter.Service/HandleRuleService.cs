using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class HandleRuleService
        : GenericManagerBase<Domain.HandleRule>, IHandleRuleService
    {
        public Shijia.Dao.IHandleRuleDao HandleRuleDao { set; get; }

        public IQueryable<Domain.HandleRule> LoadAllByAccountID(int accountID)
        {
            return HandleRuleDao.LoadAllByAccountID(accountID);
        }
        public IQueryable<Domain.HandleRule> LoadAllByKeyWord(int accountID, string keyWord)
        {
            return HandleRuleDao.LoadAllByKeyWord(accountID, keyWord);
        }
    }
}