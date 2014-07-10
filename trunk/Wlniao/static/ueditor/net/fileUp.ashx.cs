using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;

namespace ZuoCo.Manage.Web.ueditor.net
{
    /// <summary>
    /// fileUp 的摘要说明
    /// </summary>
    public class fileUp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //上传配置                                     //保存路径
            string[] filetype = { ".rar", ".doc", ".docx", ".zip", ".pdf", ".txt", ".swf", ".wmv" };    //文件允许格式
            int size = 100;   //文件大小限制,单位MB,同时在web.config里配置环境默认为100MB


            //上传文件
            Hashtable info = new Hashtable();
            UEditor.Uploader up = new UEditor.Uploader();
            info = up.upFile(context, filetype, size); //获取上传状态

            context.Response.Write("{'state':'" + info["state"] + "','url':'" + info["url"] + "','fileType':'" + info["currentType"] + "','original':'" + info["originalName"] + "'}"); //向浏览器返回数据json数据
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}