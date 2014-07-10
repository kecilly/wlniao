using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Service
{
    public interface IAccountService : IGenericManager<Domain.Account>
    {
        Shijia.Dao.IAccountDao AccountDao { set; get; }

        IList<Domain.Account> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        Domain.Account Get(string userName);

        Domain.Account Get(string userName, string password);
        Result CheckLogin(String username, String password);
        void Update(Domain.Account entity, string password);
        Domain.Account GetByUserName(String UserName);
        Result Add(String username, String password, String email = "", String mobile = "");
        Result ChangePwd(String username, String password, String oldpassword = "");
        Result ChangeStop(String username);
        IList<Domain.Account> GetPage(string where = "", int pageindex = 0, int pagesize = 10);
    }
}
