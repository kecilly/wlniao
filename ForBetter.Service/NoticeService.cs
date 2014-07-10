using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class NoticeService
        : GenericManagerBase<Domain.Notice>, INoticeService
    {
        public Shijia.Dao.INoticeDao NoticeDao { set; get; }

        public IQueryable<Domain.Notice> LoadALL(int accountID)
        {
            return NoticeDao.LoadALL(accountID);
        }
    }
}