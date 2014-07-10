using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao
{
    public partial class change : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //#region 栏目内容
            //string classpath = "UsersData/jnhwwh/News/";
            //string[] usernewskeys = Oss.GetFiles(classpath);

            //foreach (string nkey in usernewskeys)
            //{
            //    if (nkey.EndsWith(".data"))
            //    {
            //        try
            //        {
            //            string njson = Oss.ReadStr(nkey);
            //            Wlniao.Model.CmsNews nmodel = Json.ToObject<Wlniao.Model.CmsNews>(njson);
            //            string temp = Oss.ReadStr("UsersData/jnhwwh/GuidContent/" + nmodel.GuidContent + ".data");
            //            if (!string.IsNullOrEmpty(temp))
            //            {
            //                temp = temp.Replace("/static/ueditor/net/_ueditor/Pic/", "http://static.weback.cn/_ueditor/Pic/");
            //                Oss.WriteStr("UsersData/jnhwwh/GuidContent/" + nmodel.GuidContent + ".data", temp);
            //            }
            //        }
            //        catch { }
            //    }
            //}
            //#endregion 

        }
    }
}