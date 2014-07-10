using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shijia.Domain;

namespace Shijia.Service
{
    public interface IMPUserInfoService : IGenericManager<Domain.MPUserInfo>
    {

        Shijia.Dao.IMPUserInfoDao MPUserInfoDao { set; get; }

        DataPage<MPUserInfo> LoadAllByPage(int accountID, out long total, int page, int rows, string order, string sort);

        Domain.MPUserInfo Get(String OpenId, int AccountId = 0);
        
        MPUserInfo Check(String OpenId, Int32 AccountId, String UserInfoJson = "");

        Domain.MPUserInfo GetByMobileNumber(String MobileNumber, int AccountId = 0);
    }
}
