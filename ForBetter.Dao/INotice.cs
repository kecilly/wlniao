using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface INoticeDao : IRepository<Domain.Notice>
    {
        IQueryable<Domain.Notice> LoadAllByPage(out long total, int page, int rows, string order, string sort);
        IQueryable<Domain.Notice> LoadALL(int accountID);
    }
}
