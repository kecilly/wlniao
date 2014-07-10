using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.App.Dao
{
    public interface IMPUserInfoDao : IRepository<Domain.MPUserInfo>
    {
        IQueryable<Domain.MPUserInfo> LoadAllByPage(string OpenId, out long total, int page, int rows, string order, string sort);
        Domain.MPUserInfo Get(String OpenId);
        Domain.MPUserInfo GetByMobileNumber(String MobileNumber);
        
    }
}
