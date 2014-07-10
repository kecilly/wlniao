using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using  Shijia.App.Domain;

namespace Shijia.App.Service
{
    [Spring.Stereotype.Service]
    [Spring.Transaction.Interceptor.Transaction(ReadOnly = true)]
    public class KeyValueDataService
        : GenericManagerBase<Domain.KeyValueData>, IKeyValueDataService
    {
        public Shijia.App.Dao.IKeyValueDataDao KeyValueDataDao { set; get; }
        
        //private Service.KeyValueDataService KeyValueService = new KeyValueDataService();
		
		private string GetPath()
        {
            string filename = PathHelper.Map(cfgHelper.FrameworkRoot + "data/BaseData.xml");
			if(file.Exists(filename))
			{
				return filename;
			}else{
				return "";
			}
		}

        public void Add(string key, string value)
        {
            Add(key, value, "");
        }
        public void Add(string key, string value, string description)
        {
            if (GetByKey(key) == null)
            {
                if (string.IsNullOrEmpty(GetPath()))
                {
                    try
                    {
                        KeyValueData kv = new KeyValueData();
                        kv.KeyName = key;
                        kv.KeyValue = value;
                        kv.Description = "";
                        kv.UpdateTime = DateTools.GetNow();
                        KeyValueDataDao.Save(kv);
                    }
                    catch { }
                }
                else
                {
                    List<XmlParamter> xplist = new List<XmlParamter>();
                    xplist.Add(new XmlParamter("Key", key));
                    xplist.Add(new XmlParamter("Value", value));
                    xplist.Add(new XmlParamter("Description", ""));
                    xplist.Add(new XmlParamter("UpdateTime", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss")));
                    XMLHelper.AddData(GetPath(), "KeyValue", xplist.ToArray());
                }
            }
        }
        public  void Edit(string key, string value)
        {
            Edit(key, value);
        }
        public  void Edit(string key, string value, string description)
        {
            KeyValueData kv = GetByKey(key);
            if (kv != null)
            {
                if (string.IsNullOrEmpty(GetPath()))
                {
                    try
                    {
                        kv.KeyName = key;
                        kv.KeyValue = value;
                        kv.Description = description;
                        kv.UpdateTime = DateTools.GetNow();
                        KeyValueDataDao.Update(kv);
                    }
                    catch { }
                }
                else
                {
                    XmlParamter xpKey = new XmlParamter("Key", key);
                    xpKey.Direction = System.IO.ParameterDirection.Equal;
                    XmlParamter xpValue = new XmlParamter("Value", value);
                    xpValue.Direction = System.IO.ParameterDirection.Update;
                    XmlParamter xpDescription = new XmlParamter("Description", description);
                    xpDescription.Direction = System.IO.ParameterDirection.Update;
                    XmlParamter xpUpdateTime = new XmlParamter("UpdateTime", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss"));
                    xpUpdateTime.Direction = System.IO.ParameterDirection.Update;

                    XMLHelper.UpdateData(GetPath(), "KeyValue", xpKey, xpValue, xpDescription, xpUpdateTime);
                }
            }
        }
        public void Save(string key, string value)
        {
            if (GetByKey(key) != null)
            {
                Edit(key, value, "");
            }
            else
            {
                Add(key, value, "");
            }
        }
        public KeyValueData GetByKey(string key)
        {
            KeyValueData kv = null;
            try
            {
                if (string.IsNullOrEmpty(GetPath()))
                {
                    kv = KeyValueDataDao.GetByKeyName(key);
                }
                else
                {
                    kv = new KeyValueData();
                    XmlParamter xpKey = new XmlParamter("Key", key);
                    xpKey.Direction = System.IO.ParameterDirection.Equal;
                    System.Xml.XmlNode xn = XMLHelper.GetDataOne(GetPath(), "KeyValue", xpKey);
                    if (xn == null)
                    {
                        return null;
                    }
                    else
                    {
                        kv.KeyName = xn.Attributes["Key"].Value;
                        kv.KeyValue = xn.Attributes["Value"].Value;
                        kv.Description = xn.Attributes["Description"].Value;
                        kv.UpdateTime = Convert.ToDateTime(xn.Attributes["UpdateTime"].Value);
                    }
                }
            }
            catch { }
            return kv;
        }
        public  String GetString(string key)
        {
            try
            {
                return GetByKey(key).KeyValue;
            }
            catch { }
            return "";
        }
        public  Int32 GetInt(string key)
        {
            try
            {
                return Convert.ToInt32(GetString(key));
            }
            catch
            {
                return 0;
            }
        }
        public  Boolean GetBool(string key)
        {
            try
            {
                return Convert.ToBoolean(GetString(key));
            }
            catch
            {
                return false;
            }
        }
    }
}