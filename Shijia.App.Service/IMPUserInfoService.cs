using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shijia.App.Domain;

namespace Shijia.App.Service
{
    public interface IMPUserInfoService : IGenericManager<Domain.MPUserInfo>
    {

        Shijia.App.Dao.IMPUserInfoDao MPUserInfoDao { set; get; }

      
    }
}
