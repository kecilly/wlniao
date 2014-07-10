using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class MPUserInfo : PageLogin
    {
        static ILog logger = LogManager.GetLogger(typeof(MPUserInfo));
        public Shijia.Service.IMPWechatService MPWechatService { get; set; }
        public Shijia.Service.IMPUserInfoService MPUserInfoService { get; set; }
        protected string _ListStr = "";
        protected string _PageBar = "";
        protected string _Keyword = "";
        protected string fansCount = "0";
        protected int MPType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                logger.Info("用户信息：事件" + helper.GetParam("do") + "|page:" + helper.GetParamInt("page") + "|rows:" + helper.GetParamInt("rows"));
                if (helper.GetParam("do") == "getlist" )
                {
                    long total = 0;
                    int pageindex = helper.GetParamInt("page") - 1;
                    int pageSize = helper.GetParamInt("rows");
                    if (pageindex < 0)
                    {
                        pageindex = 0;
                    }
                    if (pageSize == 0)
                    {
                        pageSize = 10;
                    }
                    try
                    {

                        var pager = MPUserInfoService.LoadAllByPage( _CurrentAccount.Id ,out total,pageindex,pageSize,"desc","SubscribeTime"); 
                        string json = Json.ToStringEx(pager);
                        helper.Response(json);
                    }
                    catch(Exception ex) {}
                }
                else if (helper.GetParam("do") == "del")
                {
                    Shijia.Domain.MPUserInfo mpuser = MPUserInfoService.Get(helper.GetParamInt("id"));
                    if (mpuser == null || mpuser.AccountId != _CurrentAccount.Id)
                    {
                        helper.Result.Add("Sorry，您要删除的用户不存在或已删除");
                    }
                    else
                    {
                        MPUserInfoService.Delete(helper.GetParamInt("id"));
                    }
                    helper.ResponseResult();
                }
                else if (helper.GetParam("do") == "getone")
                {
                    Shijia.Domain.MPUserInfo mpuser = MPUserInfoService.Get(helper.GetParamInt("id"));
                    helper.Response(mpuser);
                }
                else if (helper.GetParam("do") == "remarkname")
                {
                    Shijia.Domain.MPUserInfo mpuser = MPUserInfoService.Get(helper.GetParamInt("id"));
                    if (mpuser == null || mpuser.AccountId != _CurrentAccount.Id)
                    {
                        helper.Result.Add("Sorry，您要编辑的用户不存在或已删除");
                    }
                    else
                    {
                        mpuser.RemarkName = helper.GetParam("RemarkName");
                       
                        MPUserInfoService.Update(mpuser);
                    }
                    helper.ResponseResult();
                }
                else if (helper.GetParam("do") == "mpuserinfosave")
                {
                    Shijia.Domain.MPUserInfo mpuser = MPUserInfoService.Get(helper.GetParamInt("id"));
                    if (mpuser == null || mpuser.AccountId != _CurrentAccount.Id)
                    {
                        helper.Result.Add("Sorry，您要编辑的用户不存在或已删除");
                    }
                    else
                    {
                        mpuser.RemarkName = helper.GetParam("RemarkName");
                        mpuser.MobileNumber = helper.GetParam("MobileNumber");
                        mpuser.Sex = helper.GetParamInt("Sex");
                        mpuser.Province = helper.GetParam("Province");
                        mpuser.City = helper.GetParam("City");
                        MPUserInfoService.Update(mpuser);
                    }
                    helper.ResponseResult();
                }
                else
                {
                    Shijia.Domain.MPWechat mpwechat = MPWechatService.Check(_CurrentAccount.Id);
                    MPType = mpwechat.MPType;

                    //long total = 0;
                    //int pageindex = helper.GetParamInt("page") - 1;
                    //int pageSize = helper.GetParamInt("rows");
                    //if (pageindex < 0)
                    //{
                    //    pageindex = 0;
                    //}
                    //if (pageSize == 0)
                    //{
                    //    pageSize = 10;
                    //}
                    //try
                    //{

                    //    var pager = MPUserInfoService.LoadAllByPage(_CurrentAccount.Id, out total, pageindex, pageSize, "desc", "SubscribeTime");
                    //    string json = Json.ToStringEx(pager);
                    //    helper.Response(json);
                    //}
                    //catch (Exception ex) { }
                }
            }
        }

    }
}