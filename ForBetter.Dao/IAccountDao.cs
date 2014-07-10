using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IAccountDao : IRepository<Domain.Account>
    {
        IQueryable<Domain.Account> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        Domain.Account Get(string userName);
    }
}
