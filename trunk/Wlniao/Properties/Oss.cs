using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
namespace Wlniao
{
    /// <summary>
    /// Oss访问（阿里云）
    /// </summary>
    public class Oss
    {
        public static string DataUrl = cfgHelper.GetAppSettings("DataUrl");
        //public Shijia.Service.IKeyValueDataService KeyValueDataService { set; get; }

        public static bool Exists(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                return System.file.Exists(key);
            }
            catch { return false; }
        }
        public static string ReadStr(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                return System.file.Read(key);
            }
            catch { return ""; }
        }
        public static void Upload(string key, System.IO.Stream content)
        {
            try
            {
                key = PathHelper.Map(key);
                using (System.IO.StreamWriter objwrite = new System.IO.StreamWriter(key))
                {
                    int k = 0;
                    while (k != -1)
                    {
                        k = content.ReadByte();
                        if (k != -1)
                        {
                            objwrite.BaseStream.WriteByte((byte)k);
                        }
                    }
                }
            }
            catch { }
        }
        public static void WriteStr(string key, string str)
        {
            key = PathHelper.Map(key);
            System.file.Write(key, str);
        }
        public static void WriteStrWithNoThread(string key, string str)
        {
            key = PathHelper.Map(key);
            System.file.Write(key, str);
        }
        private static void WriteStrThread(Object obj)
        {
            try
            {
                Shijia.Domain.KeyValueData kv = (Shijia.Domain.KeyValueData)obj;
                kv.KeyName = PathHelper.Map(kv.KeyName);
                System.file.Write(kv.KeyValue, kv.KeyName);
            }
            catch { }
        }
        public static string[] GetFiles(string key)
        {
            try
            {
                key = PathHelper.Map(key);
                string[] files = System.IO.Directory.GetFiles(key);
                return files;
            }
            catch
            {
                return new string[] { };
            }
        }
        public static void Delete(string key)
        {
            key = PathHelper.Map(key);
            System.file.Delete(key);
        }
        public static void MoveTo(string sourceKey, string destinationKey)
        {
            sourceKey = PathHelper.Map(sourceKey);
            destinationKey = PathHelper.Map(destinationKey);
            System.file.Move(sourceKey, destinationKey);
        }
        public static void Copy(string sourceKey, string destinationKey)
        {
            sourceKey = PathHelper.Map(sourceKey);
            destinationKey = PathHelper.Map(destinationKey);
            System.file.Copy(sourceKey, destinationKey);
        }
    }
}
