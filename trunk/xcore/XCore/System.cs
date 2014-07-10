using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

//using System.ORM;
using System.Data;
//using System.ORM.Operation;
using System.Web;
//using System.ORM.Caching;
namespace System {

    /// <summary>
    /// XCenter Jsonת�����ߡ�
    /// </summary>
    public class Json
    {
        /// <summary>
        /// ��ȡ��ֵ����ĳһ�ֶε�ֵ
        /// </summary>
        /// <param name="map">��ֵ��</param>
        /// <param name="field">�ֶ�����</param>
        /// <returns></returns>
        public static Object GetField(Dictionary<String, object> map, String field)
        {
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// ��ȡ��ֵ����ĳһ�ֶε�ֵ
        /// </summary>
        /// <param name="map">��ֵ��</param>
        /// <param name="field">�ֶ�����</param>
        /// <returns></returns>
        public static String GetFieldStr(Dictionary<String, object> map, String field)
        {
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// ��ȡ json �ַ�����ĳһ�ֶε�ֵ
        /// </summary>
        /// <param name="oneJsonString">json �ַ���</param>
        /// <param name="field">�ֶ�����</param>
        /// <returns></returns>
        public static Object GetField(String oneJsonString, String field)
        {

            Dictionary<String, object> map = System.Serialization.JsonParser.Parse(oneJsonString) as Dictionary<String, object>;
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// ��ȡ json �ַ�����ĳһ�ֶε�ֵ
        /// </summary>
        /// <param name="oneJsonString">json �ַ���</param>
        /// <param name="field">�ֶ�����</param>
        /// <returns></returns>
        public static String GetFieldStr(String oneJsonString, String field)
        {

            Dictionary<String, object> map = System.Serialization.JsonParser.Parse(oneJsonString) as Dictionary<String, object>;
            foreach (KeyValuePair<String, object> pair in map)
            {
                if (pair.Key == field)
                {
                    return pair.Value.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// �� json �ַ��������л�Ϊ����
        /// </summary>
        /// <param name="oneJsonString">json �ַ���</param>
        /// <param name="t">Ŀ������</param>
        /// <returns></returns>
        public static Object ToObject(String oneJsonString, Type t)
        {

            Dictionary<String, object> map = System.Serialization.JsonParser.Parse(oneJsonString) as Dictionary<String, object>;
            return System.Serialization.JSON.setValueToObject(t, map);
        }

        /// <summary>
        /// �� json �ַ��������л�Ϊ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json �ַ���</param>
        /// <returns></returns>
        public static T ToObject<T>(String jsonString)
        {
            Object result = ToObject(jsonString, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// ���������л�Ϊjson�ַ���,��֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToString(Object obj)
        {
            return System.Serialization.SimpleJsonString.ConvertObject(obj);
        }
        /// <summary>
        /// ���������л�Ϊjson�ַ���,֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToStringEx(Object obj)
        {
            return System.Serialization.JsonString.Convert(obj);
        }
        /// <summary>
        /// ���������л�Ϊjson�ַ���,֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToStringEx(Object obj, params Shijia.Domain.KeyValueData[] kvs)
        {
            string s = System.Serialization.JsonString.Convert(obj);
            if (kvs != null && kvs.Length > 0)
            {
                string strPx = "";
                foreach (Shijia.Domain.KeyValueData kv in kvs)
                {
                    strPx += "\"" + kv.KeyName + "\":\"" + kv.KeyValue + "\",";
                }
                string temp = s.Substring(1).Trim();
                if (temp == "}")
                {
                    strPx = strPx.Substring(0, strPx.Length - 1);
                }
                s = "{" + strPx + temp;
            }
            return s;
        }
        /// <summary>
        /// �����󼯺����л�Ϊjson�ַ���,��֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToStringList(IList list)
        {
            return System.Serialization.SimpleJsonString.ConvertList(list);
        }

        /// <summary>
        /// �� json �ַ��������л�Ϊ�����б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json �ַ���</param>
        /// <returns>���ض����б�</returns>
        public static List<T> ToList<T>(String jsonString)
        {

            List<T> list = new List<T>();
            if (System.strUtil.IsNullOrEmpty(jsonString)) return list;
            List<object> lists = System.Serialization.JsonParser.Parse(jsonString) as List<object>;
            if (lists != null)
            {
                foreach (Dictionary<String, object> map in lists)
                {
                    Object result = System.Serialization.JSON.setValueToObject(typeof(T), map);
                    list.Add((T)result);
                }
            }
            return list;
        }
    }
    public class NetTool
    {        //�õ����ص�ַ
        public static string GetGateway()
        {
            //���ص�ַ
            string strGateway = "";
            //��ȡ��������
            System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //��������
            foreach (var netWork in nics)
            {
                //����������IP����
                System.Net.NetworkInformation.IPInterfaceProperties ip = netWork.GetIPProperties();
                //��ȡ��IP���������
                System.Net.NetworkInformation.GatewayIPAddressInformationCollection gateways = ip.GatewayAddresses;
                foreach (var gateWay in gateways)
                {
                    //����ܹ�Pingͨ����
                    if (IsPingIP(gateWay.Address.ToString()))
                    {
                        //�õ����ص�ַ
                        strGateway = gateWay.Address.ToString();
                        //����ѭ��
                        break;
                    }
                }
                //����Ѿ��õ����ص�ַ
                if (strGateway.Length > 0)
                {
                    //����ѭ��
                    break;
                }
            }
            //�������ص�ַ
            return strGateway;
        }/// <summary>
        /// ����Pingָ��IP�Ƿ��ܹ�Pingͨ
        /// </summary>
        /// <param name="strIP">ָ��IP</param>
        /// <returns>true �� false ��</returns>
        public static bool IsPingIP(string strIP)
        {
            try
            {
                //����Ping����
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                //����Ping����ֵ
                System.Net.NetworkInformation.PingReply reply = ping.Send(strIP, 1000);
                //Pingͨ
                return true;
            }
            catch
            {
                //Pingʧ��
                return false;
            }
        }
    }

    //public class Tool
    //{
    //    /// <summary>
    //    /// ��ȡConfiger���ݣ��Ȳ���KeyValue���ݣ��ٲ���Web.Config��
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public static string GetConfiger(string key)
    //    {
    //        KvTableUtil KvTableUtil1 = new KvTableUtil();
    //        string str = KvTableUtil1.GetString(key);
    //        if (string.IsNullOrEmpty(key))
    //        {
    //            str = System.Configuration.ConfigurationManager.AppSettings[key];
    //        }
    //        return str;
    //    }
    //}

    /// <summary>
    /// �����������
    /// </summary>
    public class Rand
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
                result += random.Next(10).ToString();
            return result;
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }
        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
    }

    public class DateTools
    {
        private static int timezone = 8;
        public static DateTime GetNow()
        {
            //if (timezone == 0)
            //{
            //    try
            //    {
            //        if (file.Exists(System.Data.DbConfig.getConfigPath()))
            //        {
            //            KvTableUtil KvTableUtil1 = new KvTableUtil();
            //            string _timezone = KvTableUtil1.GetString("TimeZone");
            //            if (string.IsNullOrEmpty(_timezone))
            //            {
            //                timezone = 8;
            //                KvTableUtil1.Save("TimeZone", "8");
            //            }
            //            else
            //            {
            //                timezone = Convert.ToInt16(_timezone);
            //            }
            //        }
            //    }
            //    catch { }
            //}
            //return DateTime.UtcNow.AddHours(timezone);// ��UTCʱ��Ϊ׼��ʱ���
            return DateTime.Now;
        }
        public static Int64 GetValidityNum()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static Int64 GetValidityNum(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }

        /// <summary>
        /// ��nuix�е����ڸ�ʽת�����������ڸ�ʽ��ǰ�ᴫ��ĸ�ʽ��ȷ
        /// </summary>
        /// <param name="timestampString">�����ʱ���</param>
        /// <returns></returns>
        public static String ConvertToWin(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// ��nuix�е����ڸ�ʽת��������ʱ��
        /// </summary>
        /// <param name="timestampString">�����ʱ���</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.Add(new TimeSpan(timestamp * 10000000));
        }
        /// <summary>
        /// ��nuix�е����ڸ�ʽת��������ʱ��
        /// </summary>
        /// <param name="timestampString">�����ʱ���</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(String timestampString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestampString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// ����ǰ����ʱ��ת����unix����ʱ�����ʽ
        /// </summary>
        /// <returns>unixʱ��</returns>
        public static string ConvertToUnix()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow =DateTime.Now.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// ����ǰ����ʱ��ת����unix����ʱ�����ʽ
        /// </summary>
        /// <returns>unixʱ��</returns>
        public static long ConvertToUnixofLong()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = DateTime.Now.Subtract(dtStart);
            return toNow.Ticks / 10000000;
        }
        /// <summary>
        /// ������������ת����unix����ʱ�����ʽ
        /// </summary>
        /// <param name="datetime">��������ת���ɵ��ַ�����ʽ�磺yyyy-MM-dd HH:mm:ss</param>
        /// <returns>unixʱ��</returns>
        public static string ConvertToUnix(DateTime datetime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = datetime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        public static String GetDayOfWeek(DateTime now)
        {
            switch (Convert.ToInt32(now.DayOfWeek))
            {
                case 0: return "����";
                case 1: return "��һ";
                case 2: return "�ܶ�";
                case 3: return "����";
                case 4: return "����";
                case 5: return "����";
                case 6: return "����";
            }
            return string.Empty;
        }
    }
}
