using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Service
{
    public interface IHandleRuleService : IGenericManager<Domain.HandleRule>
    {
        Shijia.Dao.IHandleRuleDao HandleRuleDao { set; get; }
        IQueryable<Domain.HandleRule> LoadAllByAccountID(int accountID);
        IQueryable<Domain.HandleRule> LoadAllByKeyWord(int accountID, string keyWord);
    }
}
