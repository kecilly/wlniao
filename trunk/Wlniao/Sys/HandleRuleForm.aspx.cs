using System;
using System.Collections.Generic;
using Shijia.Domain;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wlniao.Sys
{
    public partial class HandleRuleForm : PageLogin
    {
        public Shijia.Service.IHandleRuleService HandleRuleService { get; set; }
        protected string _dataurl = Oss.DataUrl;
        protected string Id;
        protected string AccountUserName;
        protected string KeyWord;
        protected string Description;
        protected string GetMode;
        protected string MsgType;
        protected string initScript;
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountUserName = _CurrentAccount.AccountUserName;
            Id = helper.GetParam("Id");
            if (!string.IsNullOrEmpty(Id))
            {
                Shijia.Domain.HandleRule model = HandleRuleService.Get(helper.GetParamInt("Id"));
                KeyWord = model.KeyWord;
                Description = model.Description;
                GetMode = model.GetMode.ToString();
                MsgType = model.MsgType.ToString();

                if (model.MsgType == 1)
                {
                    try
                    {
                        string[] msgs = model.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string msg in msgs)
                        {
                            initScript += "txtItemAdd('" + msg.Replace("\'", "\\\'") + "');\n";
                        }
                    }
                    catch { }
                }
                else if (model.MsgType == 2)
                {
                    try
                    {
                        string newstitle = "";
                        string newsdesc = "";
                        string newspicurl = "";
                        string newspiclink = "";
                        string newscontent = "";
                        string[] msgs = model.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                        int i = 0;
                        foreach (string msg in msgs)
                        {
                            string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                            try
                            {
                                newstitle = kv[0].Replace("'", "\\'");
                            }
                            catch { newstitle = ""; }
                            try
                            {
                                newsdesc = kv[1].Replace("'", "\\'");
                            }
                            catch { newsdesc = ""; }
                            try
                            {
                                newspicurl = kv[2].Replace("\\", "/").Replace("'", "\\'");
                            }
                            catch { newspicurl = ""; }
                            try
                            {
                                newspiclink = kv[3].Replace("\\", "/").Replace("'", "\\'");
                            }
                            catch { newspiclink = ""; }
                            try
                            {
                                newscontent = kv[4].Replace("'", "\\'");
                            }
                            catch { newscontent = ""; }
                            if (i == 0)
                            {
                                initScript += "newsMainSet('" + newstitle + "','" + newsdesc + "','" + newspicurl + "','" + newspiclink + "','" + newscontent + "');\n";
                            }
                            else
                            {
                                initScript += "newsItemAdd('" + newstitle + "','" + newsdesc + "','" + newspicurl + "','" + newspiclink + "','" + newscontent + "');\n";
                            }
                            i++;
                        }
                    }
                    catch { }
                }
                else if (model.MsgType == 3)
                {
                    try
                    {
                        string musictitle = "";
                        string musicurl = "";
                        string musichdurl = "";
                        string musicdesc = "";
                        string[] msgs = model.Content.Split(new string[] { "#@@@#" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string msg in msgs)
                        {
                            string[] kv = msg.Split(new string[] { "#@@#" }, StringSplitOptions.None);
                            if (kv[0] == "musictitle")
                            {
                                try
                                {
                                    musictitle = kv[1].Replace("'", "\\'");
                                }
                                catch { musictitle = ""; }
                            }
                            else if (kv[0] == "musicurl")
                            {
                                try
                                {
                                    musicurl = kv[1].Replace("\\", "/").Replace("'", "\\'");
                                }
                                catch { musicurl = ""; }
                            }
                            else if (kv[0] == "musichdurl")
                            {
                                try
                                {
                                    musichdurl = kv[1].Replace("\\", "/").Replace("'", "\\'");
                                }
                                catch { musichdurl = ""; }
                            }
                            else if (kv[0] == "musicdesc")
                            {
                                try
                                {
                                    musicdesc = kv[1].Replace("'", "\\'");
                                }
                                catch { musicdesc = ""; }
                            }
                        }
                        initScript += "initMusic('" + musictitle + "','" + (musicurl.StartsWith("http://") ? musicurl : Oss.DataUrl + "/" + musicurl) + "','" + (musichdurl.StartsWith("http://") ? musichdurl : Oss.DataUrl + "/" + musichdurl) + "','" + musicdesc + "');";
                    }
                    catch { }
                }
                else if (model.MsgType == 999)
                {
                    try
                    {
                        if (model.Content.ToLower().StartsWith("api:"))
                        {
                            initScript += "apiItemSet('" + model.Content.Substring(4) + "');\n";
                        }
                    }
                    catch { }
                }
            }
        }

    }
}