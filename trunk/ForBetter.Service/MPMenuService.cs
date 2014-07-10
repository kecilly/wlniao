using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class MPMenuService
        : GenericManagerBase<Domain.MPMenu>, IMPMenuService
    {
        public Shijia.Dao.IMPMenuDao MPMenuDao { set; get; }

        public int GetNewSequense(Int32 AccountId)
        {
            try
            {
                return MPMenuDao.GetByAccount(AccountId).FirstOrDefault().MPMenuSequense.Value + 1;
            }
            catch { }
            return 1;
        }

        public void UpdateSeed(Int32 AccountId, Int32 Sequense)
        {
            try
            {
                MPMenu mpmenu = MPMenuDao.GetByAccount(AccountId).FirstOrDefault();
                mpmenu.MPMenuSequense = Sequense;
                MPMenuDao.Update(mpmenu);
            }
            catch { }
        }

        public  String GetTreeData(Int32 AccountId)
        {
            try
            {
                return MPMenuDao.GetByAccount(AccountId).FirstOrDefault().MPTreeData;
            }
            catch { }
            return "";
        }
        public  String GetMenuData(Int32 AccountId)
        {
            try
            {
                return MPMenuDao.GetByAccount(AccountId).FirstOrDefault().MPMenuData;
            }
            catch { }
            return "";
        }

        public  int SaveTreeData(Int32 AccountId, string data)
        {
            MPMenu mpmenu = MPMenuDao.GetByAccount(AccountId).FirstOrDefault();
            if (mpmenu == null)
            {
                mpmenu = new MPMenu();
                mpmenu.AccountId = AccountId;
                mpmenu.MPTreeData = data;
                MPMenuDao.Save( mpmenu);
            }
            else
            {
                mpmenu.MPTreeData = data;
                MPMenuDao.Update(mpmenu);
            }
            return 0;
        }
        public  int SaveMenuData(Int32 AccountId, string data)
        {
            MPMenu mpmenu = MPMenuDao.GetByAccount(AccountId).FirstOrDefault();
            if (mpmenu == null)
            {
                mpmenu = new MPMenu();
                mpmenu.AccountId = AccountId;
                mpmenu.MPMenuData = data;
                MPMenuDao.Save( mpmenu);
            }
            else
            {
                mpmenu.MPMenuData = data;
                MPMenuDao.Update(mpmenu);
            }
            return 0;
        }
    


        #region WeixinMenuHelper
        public class WeixinMenuHelper
        {
            #region 由原数据格式解析成微信菜单json格式
            /// <summary>
            /// 由原数据格式解析成微信菜单json格式
            /// </summary>
            /// <param name="data"></param>
            public static string Parse(string data)
            {
                WXMenu menu = new WXMenu();
                //1 反序列化为jsonarray 数组，不过数组集合中始终只有1条对象
                List<WXMenuButton> buttons = Json.ToList<WXMenuButton>(data);
                if (buttons != null && buttons.Count > 0)
                {
                    #region Parse 解析成微信 json格式

                    for (int i = 0; i < buttons.Count; i++)         //遍历jsonarray               
                    {
                        if (buttons[i].children.Count > 0)
                        {
                            //定义包含子菜单的菜单
                            WXMenuComplexButton cxb = new WXMenuComplexButton();
                            cxb.name = buttons[i].text;

                            #region
                            //循环添加用户子菜单项
                            for (int j = 0; j < buttons[i].children.Count; j++)
                            {
                                WXMenuCommonButton cb = new WXMenuCommonButton();
                                cb.name = buttons[i].children[j].text;
                                cb.type = buttons[i].children[j].type;
                                if (cb.type == "click")
                                {
                                    cb.key = buttons[i].children[j].key;
                                    cb.url = "";
                                }
                                else
                                {
                                    cb.key = "";
                                    cb.url = buttons[i].children[j].key;
                                }

                                //添加
                                cxb.sub_button.Add(cb);
                            }
                            #endregion

                            //添加进菜单
                            menu.button.Add(cxb);
                        }
                        else
                        {
                            //定义普通菜单并添加菜单
                            WXMenuCommonButton cb = new WXMenuCommonButton();
                            cb.name = buttons[i].text;
                            cb.type = buttons[i].type;
                            if (cb.type == "click")
                            {
                                cb.key = buttons[i].key;
                                cb.url = "";
                            }
                            else
                            {
                                cb.key = "";
                                cb.url = buttons[i].key;
                            }
                            menu.button.Add(cb);
                        }
                    }
                    #endregion
                }
                var jsondata = Json.ToStringEx(menu);
                return jsondata;
            }
            #endregion
        }
        #endregion

    }

    #region StringExtend
    internal static class StringExtend
    {
        public static int ToInt32(this string obj)
        {
            int v = 0;
            if (string.IsNullOrEmpty(obj))
                return v;

            Int32.TryParse(obj, out v);
            return v;
        }
    }
    #endregion
}