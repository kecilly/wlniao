using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Rest.WX
{
    public partial class SetToken : PageLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Rand.Str(12).ToLower();
            //helper.Result.Join(Wlniao.Model.DB.MPWechat.SetToken(_CurrentAccount.Id, token));
            helper.Add("token", token);
            helper.ResponseResult();
        }
    }
}