using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.Domain
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
            /// Éú³Éencode×Ö·û´®
            /// </summary>
            /// <returns></returns>
            public string ToEncodedString()
            {
                return string.Format("{0}={1}", KeyName, EncodedValue);
            }
	}
}