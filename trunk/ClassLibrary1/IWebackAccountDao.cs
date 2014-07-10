using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.App.Dao
{
    public interface IAccountDao : IRepository<Domain.WebackAccount>
    {
        IQueryable<Domain.WebackAccount> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        Domain.WebackAccount Get(string AppUserId);
    }
}
