using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shijia.Domain;

namespace Shijia.Service
{
    public interface IMPWechatService : IGenericManager<Domain.MPWechat>
    {
        Shijia.Dao.IMPWechatDao MPWechatDao { set; get; }

        Domain.MPWechat GetByAccountID(int accountID);

        /// <summary>
        /// 检查公众帐号的配置信息（没有则自动创建）
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        MPWechat Check(int AccountId);

        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        Result SetToken(Int32 AccountId, String Token);

        /// <summary>
        /// 更新智能匹配的关键词列表
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result UpdateKeyWordsList(Int32 AccountId);

         /// <summary>
        /// 获取处理规则
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        HandleRule Get(Int32 AccountId, String KeyWord);

        /// <summary>
        /// 根据用户消息去获取自动回复规则
        /// </summary>
        /// <param name="wechat"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        HandleRule GetByText(MPWechat wechat, String text);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result Init(Int32 AccountId);

         /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result NewVerify(Int32 AccountId, String VerifyContent);

         /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result NewVerify(Int32 AccountId, String VerifyContent, String Token);

        /// <summary>
        /// 初始化验证
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result Verify(MPWechat wechat, String FristAccount, String ManagerOpenid);

        /// <summary>
        /// 清除强制初始化
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Result ClearVerify(Int32 AccountId);

        /// <summary>
        /// 设置首次关注时推送的消息
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WelcomeMsg"></param>
        /// <returns></returns>
        Result SetWelcomeMsg(Int32 AccountId, String WelcomeMsg);

         /// <summary>
        /// 设置无关键词时的默认回复
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="DefaultMsg"></param>
        /// <returns></returns>
        Result SetDefaultMsg(Int32 AccountId, String DefaultMsg);

         /// <summary>
        /// 同时设置首次关注时推送的消息和无关键词时的默认回复
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WelcomeMsg"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        Result SetWelcomeAndDefault(Int32 AccountId, String WelcomeMsg, String DefaultMsg);

         /// <summary>
        /// 同时设置原始ID和管理员OpenId
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="FristAccount"></param>
        /// <param name="ManagerOpenid"></param>
        /// <returns></returns>
        Result SetFristAccountAndManager(Int32 AccountId, String FristAccount, String ManagerOpenid);

        /// <summary>
        /// 设置微信名和Token
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="WeixinName"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        Result SetNameAndToken(Int32 AccountId, String WeixinName, String Token);

        /// <summary>
        /// 设置AppId和Secret
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="AppId"></param>
        /// <param name="Secret"></param>
        /// <param name="MPType">公众帐号类型</param>
        /// <returns></returns>
        Result SetAppIdAndSecret(Int32 AccountId, String AppId, String AppSecret, Int32 MPType = 0);
    }
}
