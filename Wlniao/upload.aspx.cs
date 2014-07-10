using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.SessionState;

namespace Wlniao
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = "";
            try
            {
                account = Session["Account"].ToString();
            }
            catch { account = ""; }
            if (string.IsNullOrEmpty(account))
            {
                account = Request["account"];
            }
            Response.ContentType = "text/plain";
            string result = UpLoadFile(Context, account);
            Response.Write(result);
        }
        /// <summary>
        /// 上传文件 方法
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="toFilePath"></param>
        /// <returns>返回上传处理结果   格式说明： 0|file.jpg|msg   成功状态|文件名|消息    </returns>
        public string UpLoadFile(HttpContext context, string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return "0|errorfile|" + "文件上传失败,错误原因：您尚未登录或登录超时!";
            }
            try
            {
                HttpPostedFile uploadFile = null;
                try
                {
                    uploadFile = context.Request.Files["Filedata"];
                }
                catch (HttpException ex)
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：服务器不能接受您的文件!";
                }
                string fileType = context.Request["filetype"];

                string _path = "";
                string toFilePath = "UsersData/" + account;
                //文件为空
                if (uploadFile == null || string.IsNullOrEmpty(uploadFile.FileName))
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：未选择任何文件！";
                }
                //获取要保存的文件信息
                FileInfo file = new FileInfo(uploadFile.FileName);
                //获得文件扩展名
                string fileNameExt = file.Extension;

                //验证合法的文件
                if (CheckFileExt(fileNameExt, fileType))
                {
                    toFilePath += "/" + fileType.Replace('.', ' ').Trim() + "/" + DateTime.Now.ToString("yyyyMM") + "/";
                    //生成将要保存的随机文件名
                    string fileName = Rand.Str(8).ToLower() + fileNameExt;

                    //获得要保存的文件路径
                    string serverFileName = toFilePath + fileName;
                    string serverFileNameThumb = toFilePath + "Thumb_" + fileName;
                    string serverPath = PathHelper.Map(serverFileName);

                    System.IO.DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(serverPath));
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    uploadFile.SaveAs(serverPath);

                    if (IsPic(fileNameExt))
                    {
                        try
                        {
                            //System.Util.MakeThumbnail(PathHelper.Map(serverFileName), PathHelper.Map(serverFileNameThumb), 80, 80, "HW");
                            return "1|" + serverFileName + "|" + "恭喜你，文件上传成功!|" + serverFileNameThumb;
                        }
                        catch
                        {
                            return "1|" + serverFileName + "|" + "恭喜你，文件上传成功，但生成缩略图错误!";
                        }
                    }
                    return "1|" + serverFileName + "|" + "恭喜你，文件上传成功!";
                }
                else
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：您选择的文件格式错误";
                }
            }
            catch (DirectoryNotFoundException e)
            {
                return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
            }
            catch (Exception e)
            {
                //return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
                return "0|errorfile|" + "文件上传失败,错误原因：" + e.Message;
            }
        }

        private bool CheckFileExt(string ext, string type)
        {
            //string extlist = Tool.GetConfiger("UploadExt");
            string extlist = "";
            if (string.IsNullOrEmpty(extlist) || extlist.Contains(ext))
            {
                if (type == "pic")
                {
                    return IsPic(ext);
                }
                else if (type == "audio")
                {
                    return IsAudio(ext);
                }
            }
            return false;
        }

        private bool IsPic(string ext)
        {
            if (".jpg,.gif,.png".Contains(ext))
            {
                return true;
            }
            return false;
        }
        private bool IsAudio(string ext)
        {
            if (".mp3,.avi,.rm".Contains(ext))
            {
                return true;
            }
            return false;
        }
    }
}