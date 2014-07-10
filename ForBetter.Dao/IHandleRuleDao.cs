using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IHandleRuleDao : IRepository<Domain.HandleRule>
    {
        IQueryable<Domain.HandleRule> LoadAllByPage(int accountID, out long total, int page, int rows, string order, string sort);
        IQueryable<Domain.HandleRule> LoadAllByPage(int accountID, int level,out long total, int page, int rows, string order, string sort);
        IQueryable<Domain.HandleRule> LoadAllByAccountID(int accountID);
        IQueryable<Domain.HandleRule> LoadAllByKeyWord(int accountID, string keyWord);
    }
}
