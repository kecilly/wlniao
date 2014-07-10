using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Linq.Functions;

namespace Wlniao
{
    public partial class appbox : PageLogin
    {
        protected string _clientid = "";
        protected string _sercetcheck = "no";
        protected string _clientaccountid = "";
        protected string _clientaccountname = "";
        
        public Shijia.Service.IKeyValueDataService KeyValueDataService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientid = KeyValueDataService.GetString("AppClientId");
            string _sercet = KeyValueDataService.GetString("AppSercet");

            if (!string.IsNullOrEmpty(Request["iheight"]))
            {
                Response.Clear();
                Response.Write("<html>\n");
                Response.Write("<head>\n");
                Response.Write("<script type=\"text/javascript\">\n");
                Response.Write("    function pseth() {\n");
                Response.Write("        var iObj;\n");
                Response.Write("        try{\n");
                Response.Write("        iObj = parent.parent.parent.document.getElementById('appFramePage'); //A和main同域，所以可以访问节点\n");
                Response.Write("        }catch(e){}\n");
                Response.Write("        if(!iObj){\n");
                Response.Write("        iObj = parent.parent.document.getElementById('appFramePage'); //A和main同域，所以可以访问节点\n");
                Response.Write("        }\n");
                Response.Write("        iObj.style.height = \"" + Request["iheight"] + "px\"; //操作dom\n");
                Response.Write("    }\n");
                Response.Write("    pseth();\n");
                Response.Write("</script>\n");
                Response.Write("</head>\n");
                Response.Write("<body></body>\n");
                Response.Write("<html>");
                Response.End();
            }
            else
            {
                if (string.IsNullOrEmpty(_sercet))
                {
                    _clientaccountid = _CurrentAccount.Id.ToString();
                    _clientaccountname = _CurrentAccount.AccountUserName;
                }
                else
                {
                    _sercetcheck = "yes";
                    _clientaccountid = Encryptor.DesEncrypt(_CurrentAccount.Id.ToString(), _sercet); ;
                    _clientaccountname = Encryptor.DesEncrypt(_CurrentAccount.AccountUserName, _sercet); ;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["v"]))
                {
                    Response.Redirect("http://weback.cn/MyApps.aspx?clientaccountid=" + _clientaccountid + "&clientaccountname=" + _clientaccountname + "&userid=" + _clientaccountname + "&clientid=" + _clientid + "&sercetcheck=" + _sercetcheck);
                }
            }
        }
    }
}