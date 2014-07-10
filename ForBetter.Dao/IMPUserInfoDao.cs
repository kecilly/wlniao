using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IMPUserInfoDao : IRepository<Domain.MPUserInfo>
    {
        IQueryable<Domain.MPUserInfo> LoadAllByPage(int accountID, out long total, int page, int rows, string order, string sort);
        Domain.MPUserInfo Get(String OpenId, int AccountId = 0);
        Domain.MPUserInfo GetByMobileNumber(String MobileNumber, int AccountId = 0);
        
    }
}
