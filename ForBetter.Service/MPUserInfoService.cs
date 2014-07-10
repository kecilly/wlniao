using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;
using System.ORM;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class MPUserInfoService
        : GenericManagerBase<Domain.MPUserInfo>, IMPUserInfoService
    {
        public Shijia.Dao.IMPUserInfoDao MPUserInfoDao { set; get; }


        public DataPage<MPUserInfo> LoadAllByPage(int accountID, out long total, int page, int rows, string order, string sort)
        {
            //return MPUserInfoDao.LoadAllByPage(accountID, out total, page, rows, order, sort);
            var list = MPUserInfoDao.LoadAllByPage(accountID, out total, page, rows, order, sort).ToList();
            //ObjectInfo state = new ObjectInfo(typeof(MPUserInfo));

            //state.Pager.setSize(rows);
            page++;
            //state.Pager.setCurrent(page);
            //state.Pager.RecordCount =Convert.ToInt32( total);
            //state.Pager.PageCount = Convert.ToInt32(Math.Ceiling(total/Convert.ToDouble(rows)));
            
            IPageList result = new PageList();
            result.Results = list;
            result.PageCount = Convert.ToInt32(Math.Ceiling(total / Convert.ToDouble(rows)));
            result.RecordCount = Convert.ToInt32(total);
            result.Size = rows;
            result.PageBar = new ObjectPage(result.RecordCount, result.PageCount, page).PageBar;
            result.Current = page;

            return new DataPage<MPUserInfo>(result);
        }

        public Domain.MPUserInfo Get(String OpenId, int AccountId = 0)
        {
            return MPUserInfoDao.Get(OpenId, AccountId);
        }

        public Domain.MPUserInfo GetByMobileNumber(String MobileNumber, int AccountId = 0)
        {
            return MPUserInfoDao.GetByMobileNumber(MobileNumber, AccountId);
        }

        public MPUserInfo Check(String OpenId, Int32 AccountId, String UserInfoJson = "")
        {
            MPUserInfo wechat = null;
            if (AccountId > 0)
            {
                try
                {
                    string where = "AccountId=" + AccountId;
                    if (!string.IsNullOrEmpty(OpenId))
                    {
                        where += " and OpenId='" + OpenId + "'"; ;
                    }
                    wechat = Get(OpenId,AccountId);
                    if (wechat == null)
                    {
                        wechat = new MPUserInfo();
                        wechat.AccountId = AccountId;
                        wechat.FakeId = "";
                        wechat.OpenId = OpenId;
                        wechat.SubscribeTime = DateTools.ConvertToUnixofLong();
                        wechat.CreateTime = DateTime.Now;
                        wechat.LastVisitTime = DateTime.Now.AddDays(-2);
                        CurrentRepository.SaveOrUpdate(wechat);                       
                    }
                    if (wechat.LastVisitTime < DateTime.Now.AddDays(-1) && !string.IsNullOrEmpty(UserInfoJson))
                    {
                        Dictionary<String, object> map = System.Serialization.JsonParser.Parse(UserInfoJson) as Dictionary<String, object>;
                        wechat.LastVisitTime = DateTime.Now;
                        wechat.NickName = Json.GetFieldStr(map, "nickname");
                        try
                        {
                            wechat.Sex = int.Parse(Json.GetFieldStr(map, "sex"));
                        }
                        catch { }
                        wechat.Country = Json.GetFieldStr(map, "country");
                        wechat.Province = Json.GetFieldStr(map, "province");
                        wechat.City = Json.GetFieldStr(map, "city");
                        wechat.MPLanguage = Json.GetFieldStr(map, "language");
                        wechat.HeadImgUrl = Json.GetFieldStr(map, "headimgurl");
                        try
                        {
                            wechat.SubscribeTime = long.Parse(Json.GetFieldStr(map, "subscribe_time"));
                        }
                        catch { }
                        CurrentRepository.SaveOrUpdate(wechat);  
                    }
                }
                catch { }
            }
            return wechat;
        }
    }
}