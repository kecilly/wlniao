using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IMPWechatDao : IRepository<Domain.MPWechat>
    {
        IQueryable<Domain.MPWechat> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        Domain.MPWechat GetByAccountID(int accountID);
             
    }
}
