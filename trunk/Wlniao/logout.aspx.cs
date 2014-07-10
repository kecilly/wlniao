using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["login"].Values["account"] = "";
            Response.Cookies["login"].Values["agent"] = "";
            Response.Cookies["login"].Values["manage"] = "";
        }

    }
}