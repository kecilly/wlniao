using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.App.Service
{
    public interface IWebackAccountService : IGenericManager<Domain.WebackAccount>
    {
        Shijia.App.Dao.IAccountDao AccountDao { set; get; }

        IList<Domain.WebackAccount> LoadAllByPage(out long total, int page, int rows, string order, string sort);
    }
}
