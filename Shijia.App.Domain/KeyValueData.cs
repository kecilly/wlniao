using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.App.Domain
{
	 	//KeyValueData
		public class KeyValueData
	{
	
      	/// <summary>
		/// Id
        /// </summary>
        public virtual int Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// KeyName
        /// </summary>
        public virtual string KeyName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// KeyValue
        /// </summary>
        public virtual string KeyValue
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Description
        /// </summary>
        public virtual string Description
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UpdateTime
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// EncodedValue
        /// </summary>
        public virtual string EncodedValue
        {
            get; 
            set; 
        }        
		   public KeyValueData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
           public KeyValueData(string key, Object value)
        {
            KeyName = key;
            KeyValue = value.ToString();
        }

		    public static KeyValueData Create(string key, Object value)
		    {
                return new KeyValueData(key, value);
		    }

            /// <summary>
            /// 生成encode字符串
            /// </summary>
            /// <returns></returns>
            public string ToEncodedString()
            {
                return string.Format("{0}={1}", KeyName, EncodedValue);
            }
            /// <summary>
            /// 根据Key排列
            /// </summary>
            /// <param name="kv1"></param>
            /// <param name="kv2"></param>
            /// <returns></returns>
            public static int CompareByKey(KeyValueData kv1, KeyValueData kv2)
            {
                return String.Compare(kv1.KeyName, kv2.KeyName);
            }
	}
}