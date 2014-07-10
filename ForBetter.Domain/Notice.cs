using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.Domain
{
	 	//Notice
		public class Notice
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
        /// AccountID
        /// </summary>
        public virtual int AccountID
        {
            get;
            set;
        }    


		/// <summary>
		/// NoticeTitle
        /// </summary>
        public virtual string NoticeTitle
        {
            get; 
            set; 
        }        
		/// <summary>
		/// NoticeContent
        /// </summary>
        public virtual string NoticeContent
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RangeId
        /// </summary>
        public virtual int? RangeId
        {
            get; 
            set; 
        }        
		   
	}
}