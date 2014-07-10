﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao
{
	public class MP
	{
        /// <summary>
        /// 错误码集合
        /// </summary>
        private static System.Collections.Hashtable err = new System.Collections.Hashtable();

        #region 自定义菜单
        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Result Init(string appid, string secret, out string token)
        {
            Result rlt = new Result();
            token = "";
            if (err.Count == 0)
            {
                err.Add("", "");
                err.Add(" -1", "系统繁忙");
                err.Add("0", "请求成功");
                err.Add("40001", "验证失败");
                err.Add("40002", "不合法的凭证类型");
                err.Add("40003", "不合法的OpenID");
                err.Add("40004", "不合法的媒体文件类型");
                err.Add("40005", "不合法的文件类型");
                err.Add("40006", "不合法的文件大小");
                err.Add("40007", "不合法的媒体文件id");
                err.Add("40008", "不合法的消息类型");
                err.Add("40009", "不合法的图片文件大小");
                err.Add("40010", "不合法的语音文件大小");
                err.Add("40011", "不合法的视频文件大小");
                err.Add("40012", "不合法的缩略图文件大小");
                err.Add("40013", "不合法的APPID");
                err.Add("40014", "不合法的access_token");
                err.Add("40015", "不合法的菜单类型");
                err.Add("40016", "不合法的按钮个数");
                err.Add("40017", "不合法的按钮个数");
                err.Add("40018", "不合法的按钮名字长度");
                err.Add("40019", "不合法的按钮KEY长度");
                err.Add("40020", "不合法的按钮URL长度");
                err.Add("40021", "不合法的菜单版本号");
                err.Add("40022", "不合法的子菜单级数");
                err.Add("40023", "不合法的子菜单按钮个数");
                err.Add("40024", "不合法的子菜单按钮类型");
                err.Add("40025", "不合法的子菜单按钮名字长度");
                err.Add("40026", "不合法的子菜单按钮KEY长度");
                err.Add("40027", "不合法的子菜单按钮URL长度");
                err.Add("40028", "不合法的自定义菜单使用用户");
                err.Add("41001", "缺少access_token参数");
                err.Add("41002", "缺少appid参数");
                err.Add("41003", "缺少refresh_token参数");
                err.Add("41004", "缺少secret参数");
                err.Add("41005", "缺少多媒体文件数据");
                err.Add("41006", "缺少media_id参数");
                err.Add("41007", "缺少子菜单数据");
                err.Add("42001", "access_token超时");
                err.Add("43001", "需要GET请求");
                err.Add("43002", "需要POST请求");
                err.Add("43003", "需要HTTPS请求");
                err.Add("44001", "多媒体文件为空");
                err.Add("44002", "POST的数据包为空");
                err.Add("44003", "图文消息内容为空");
                err.Add("45001", "多媒体文件大小超过限制");
                err.Add("45002", "消息内容超过限制");
                err.Add("45003", "标题字段超过限制");
                err.Add("45004", "描述字段超过限制");
                err.Add("45005", "链接字段超过限制");
                err.Add("45006", "图片链接字段超过限制");
                err.Add("45007", "语音播放时间超过限制");
                err.Add("45008", "图文消息超过限制");
                err.Add("45009", "接口调用超过限制");
                err.Add("45010", "创建菜单个数超过限制");
                err.Add("45015", "回复时间超过限制");
                err.Add("46001", "不存在媒体数据");
                err.Add("46002", "不存在的菜单版本");
                err.Add("46003", "不存在的菜单数据");
                err.Add("47001", "解析JSON/XML内容错误");
            }
            try
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
                string result = System.Web.PostAndGet.GetResponseString(url);
                try
                {
                    token = Json.ToObject<GetToken>(result).access_token;
                    if (string.IsNullOrEmpty(token))
                    {
                        rlt.Add(err["40001"].ToString());
                    }
                }
                catch
                {
                    Msg msg = Json.ToObject<Msg>(result);
                    if (msg.errcode != "0")
                    {
                        rlt.Add(err[msg.errcode].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }
        /// <summary>
        /// 同步自定义菜单
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Result SyncMenu(string appid, string secret, string json)
        {
            Result rlt = new Result();
            try
            {
                string token = "";
                rlt.Join(Init(appid, secret, out token));
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", token);
                string result = System.Web.PostAndGet.PostWebRequest(url, json, "utf-8");

                Msg msg = Json.ToObject<Msg>(result);
                if (msg.errcode != "0")
                {
                    rlt.Add(err[msg.errcode].ToString());
                }
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }
        /// <summary>
        /// 获取自定义菜单
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Result GetMenu(string appid, string secret,out string json)
        {
            json = "";
            Result rlt = new Result();
            try
            {
                string token = "";
                rlt.Join(Init(appid, secret, out token));
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", token);
                json = System.Web.PostAndGet.GetResponseString(url);
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }
        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static Result DelMenu(string appid, string secret)
        {
            Result rlt = new Result();
            try
            {
                string token = "";
                rlt.Join(Init(appid, secret, out token));
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", token);
                string result = System.Web.PostAndGet.PostWebRequest(url, "", "utf-8");

                Msg msg = Json.ToObject<Msg>(result);
                if (msg.errcode != "0")
                {
                    rlt.Add(err[msg.errcode].ToString());
                }
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }
        #endregion 自定义菜单



        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="openid"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Result GetUserInfo(string appid, string secret,string openid, out string json)
        {
            json = "";
            Result rlt = new Result();
            try
            {
                string token = "";
                rlt.Join(Init(appid, secret, out token));
                if (rlt.IsValid)
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", token, openid);
                    json = System.Web.PostAndGet.GetResponseString(url);
                }
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Result SendText(string appid, string secret, string openid,string text)
        {
            string json = "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}";
            Result rlt = new Result();
            try
            {
                string token = "";
                rlt.Join(Init(appid, secret, out token));
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", token);
                string result = System.Web.PostAndGet.PostWebRequest(url, json, "utf-8");

                Msg msg = Json.ToObject<Msg>(result);
                if (msg.errcode != "0")
                {
                    rlt.Add(err[msg.errcode].ToString());
                }
            }
            catch (Exception ex)
            {
                rlt.Add("错误：" + ex.Message);
            }
            return rlt;
        }

        public static String GetToken(string appid, string secret)
        {
            string token = "";
            Init(appid, secret, out token);
            return token;
        }
    }

	public class GetToken
	{
		private string _access_token;
		private string _expires_in;

		public string access_token { get { return _access_token; } set { _access_token = value; } }

		public string expires_in { get { return _expires_in; } set { _expires_in = value; } }
	}

	public class Msg
	{
		private string _errcode;
		private string _errmsg;

		public string errcode { get { return _errcode; } set { _errcode = value; } }

		public string errmsg { get { return _errmsg; } set { _errmsg = value; } }
	}
}
