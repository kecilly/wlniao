using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Service
{
    public interface INoticeService : IGenericManager<Domain.Notice>
    {
        Shijia.Dao.INoticeDao NoticeDao { set; get; }
        IQueryable<Domain.Notice> LoadALL(int accountID);
    }
}
