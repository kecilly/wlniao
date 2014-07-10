using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.App.Domain;
using System.ORM;

namespace Shijia.App.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class MPUserInfoService
        : GenericManagerBase<Domain.MPUserInfo>, IMPUserInfoService
    {
        public Shijia.App.Dao.IMPUserInfoDao MPUserInfoDao { set; get; }
    }
}