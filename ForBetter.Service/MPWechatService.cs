using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Shijia.Domain;

namespace Shijia.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class MPWechatService
        : GenericManagerBase<Domain.MPWechat>, IMPWechatService
    {

      
        public Domain.MPWechat GetByAccountID(int accountID)
        {
            return this.MPWechatDao.GetByAccountID(accountID);
        }

        public Shijia.Dao.IMPWechatDao MPWechatDao { set; get; }
        public Shijia.Dao.IHandleRuleDao HandleRuleDao { set; get; }

        /// <summary>
        /// 检查公众帐号的配置信息（没有则自动创建）
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public MPWechat Check(int AccountId)
        {
            MPWechat wechat = GetByAccountID(AccountId);
            if (wechat == null)
            {
                wechat = new MPWechat();
                wechat.AccountId = AccountId;
                this.CurrentRepository.Save(wechat);              
            }
            return wechat;
        }
        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public  Result SetToken(Int32 AccountId, String Token)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.Token = Token;
                    this.CurrentRepository.Update(wechat);
                    
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }

        #region 自动处理规则相关 开始
        public  string[] Separation
        {
            get
            {
                //return new string[] { " ", "#" };
                return new string[] { " " };
            }
        }
        /// <summary>
        /// 更新智能匹配的关键词列表
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result UpdateKeyWordsList(int AccountId)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);

                    List<HandleRule> list = this.HandleRuleDao.LoadAllByAccountID(AccountId).ToList();
                    string cache = "";
                    foreach (HandleRule rule in list)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(cache))
                            {
                                cache += "|";
                            }
                            cache += rule.KeyWord;
                        }
                        catch { }
                    }
                    wechat.KeyWordsList = cache;
                    this.CurrentRepository.Update(wechat);
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 获取处理规则
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public  HandleRule Get(int AccountId, String KeyWord)
        {
            try
            {
                return this.HandleRuleDao.LoadAllByKeyWord(AccountId, KeyWord).ToList().FirstOrDefault();
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 根据用户消息去获取自动回复规则
        /// </summary>
        /// <param name="wechat"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public  HandleRule GetByText(MPWechat wechat, String text)
        {
            HandleRule model = null;
            try
            {
                string Code = text.Split(Separation, StringSplitOptions.RemoveEmptyEntries)[0];
                model = this.Get(wechat.AccountId.Value,Code);
                if (model == null)
                {
                    Result result = strUtil.CheckSensitiveWords(text, wechat.KeyWordsList);
                    if (result.Errors.Count > 0)
                    {
                        foreach (string key in result.Errors)
                        {
                            model = Get(wechat.AccountId.Value,key);
                            if (model != null)
                            {
                                break;
                            }
                        }
                    }
                }
                try
                {
                    model.PushCount++;
                    this.CurrentRepository.Update(wechat);
                }
                catch { }
            }
            catch { }
            return model;
        }
        #endregion 自动处理规则相关 结束






        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result Init(int AccountId)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.Token = Rand.Str(12).ToLower();
                    wechat.ManagerOpenid = "";
                    wechat.VerifyContent = Rand.Number(6).ToLower();
                    this.CurrentRepository.Update(wechat);                  
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result NewVerify(Int32 AccountId, String VerifyContent)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.ManagerOpenid = "";
                    wechat.VerifyContent = VerifyContent;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result NewVerify(Int32 AccountId, String VerifyContent, String Token)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.Token = Token;
                    wechat.ManagerOpenid = "";
                    wechat.VerifyContent = VerifyContent;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 初始化验证
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result Verify(MPWechat wechat, String FristAccount, String ManagerOpenid)
        {
            Result rlt = new Result();
            if (wechat != null)
            {
                try
                {
                    //MPWechat wechat = Check(AccountId);
                    wechat.FristAccount = FristAccount;
                    wechat.ManagerOpenid = ManagerOpenid;
                    wechat.VerifyContent = "";
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 清除强制初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public  Result ClearVerify(Int32 AccountId)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.FristAccount = "gh_xxxxxxxxxxxx";
                    wechat.VerifyContent = "";
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }


        /// <summary>
        /// 设置首次关注时推送的消息
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WelcomeMsg"></param>
        /// <returns></returns>
        public  Result SetWelcomeMsg(Int32 AccountId, String WelcomeMsg)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.WelcomeMsg = WelcomeMsg;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 设置无关键词时的默认回复
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="DefaultMsg"></param>
        /// <returns></returns>
        public  Result SetDefaultMsg(Int32 AccountId, String DefaultMsg)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.DefaultMsg = DefaultMsg;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
        /// <summary>
        /// 同时设置首次关注时推送的消息和无关键词时的默认回复
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WelcomeMsg"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public  Result SetWelcomeAndDefault(Int32 AccountId, String WelcomeMsg, String DefaultMsg)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.WelcomeMsg = WelcomeMsg;
                    wechat.DefaultMsg = DefaultMsg;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }

        /// <summary>
        /// 同时设置原始ID和管理员OpenId
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="FristAccount"></param>
        /// <param name="ManagerOpenid"></param>
        /// <returns></returns>
        public  Result SetFristAccountAndManager(Int32 AccountId, String FristAccount, String ManagerOpenid)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.FristAccount = FristAccount;
                    wechat.ManagerOpenid = ManagerOpenid;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }

        /// <summary>
        /// 设置微信名和Token
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WeixinName"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public  Result SetNameAndToken(Int32 AccountId, String WeixinName, String Token)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.WeixinName = WeixinName;
                    wechat.Token = Token;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }

        /// <summary>
        /// 设置AppId和Secret
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="AppId"></param>
        /// <param name="Secret"></param>
        /// <param name="MPType">公众帐号类型</param>
        /// <returns></returns>
        public  Result SetAppIdAndSecret(Int32 AccountId, String AppId, String AppSecret, Int32 MPType = 0)
        {
            Result rlt = new Result();
            if (AccountId > 0)
            {
                try
                {
                    MPWechat wechat = Check(AccountId);
                    wechat.AppId = AppId;
                    wechat.AppSecret = AppSecret;
                    wechat.MPType = MPType;
                    this.CurrentRepository.Update(wechat);   
                }
                catch (Exception ex) { rlt.Add("错误：" + ex.Message); }
            }
            return rlt;
        }
    }
}