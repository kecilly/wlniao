using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IMPMenuDao : IRepository<Domain.MPMenu>
    {
        IQueryable<Domain.MPMenu> LoadAllByPage(out long total, int page, int rows, string order, string sort);
        IQueryable<Domain.MPMenu> GetByAccount(int accountID);
    }
}
