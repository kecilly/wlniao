using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace Shijia.App
{
    public class Common
    {

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        private static String UploadFile(System.IO.Stream stream, String directory, String filename)
        {
            List<Domain.KeyValueData> kvList = new List<Domain.KeyValueData>();
            kvList.Add(Domain.KeyValueData.Create("directory", directory));
            kvList.Add(Domain.KeyValueData.Create("filename", filename));


            string url = "http://weapi.weback.cn/upload.aspx";
            foreach (Domain.KeyValueData kv in kvList)
            {
                if (url.Contains("?"))
                {
                    url += "&" + kv.KeyName + "=" + kv.KeyValue;
                }
                else
                {
                    url += "?" + kv.KeyName + "=" + kv.KeyValue;
                }
            }
            string ret = string.Empty;

            Encoding encode = Encoding.UTF8;
            byte[] byteArray = new byte[stream.Length];
            stream.Read(byteArray, 0, byteArray.Length);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webReq.Method = "POST";
            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), encode);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
            return ret;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public static Cload Upload(System.IO.Stream stream, String directory, String filename)
        {
            Cload cload = new Cload();
            cload.success = false;
            string str = UploadFile(stream, directory, filename);
            if (!string.IsNullOrEmpty(str))
            {
                Dictionary<String, object> map = System.Serialization.JsonParser.Parse(str) as Dictionary<String, object>;
                cload.url = Json.GetFieldStr(map, "url");
                cload.ossurl = Json.GetFieldStr(map, "ossurl");
                cload.fullurl = Json.GetFieldStr(map, "fullurl");
                if (!string.IsNullOrEmpty(cload.fullurl))
                {
                    cload.success = true;
                }
            }
            return cload;
        }
        public class Cload
        {
            public bool success { get; set; }
            public string url { get; set; }
            public string ossurl { get; set; }
            public string fullurl { get; set; }
        }
    }
}
