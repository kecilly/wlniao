using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.Domain
{
	 	//HandleRule

		public class HandleRule
	{
            public HandleRule()
            {
                AccountId = 0;
                KeyWord = string.Empty;
                Description = string.Empty;
                GetMode = 0;
                MsgType = 0;
                RuleLevel = 0;
                PushCount = 0;
                Content = string.Empty;
            }
      	/// <summary>
		/// Id
        /// </summary>
        public virtual int Id
        {
            get; 
            set; 
        }        
		/// <summary>
		/// AccountId
        /// </summary>
        public virtual int? AccountId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// KeyWord
        /// </summary>
        public virtual string KeyWord
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
		/// GetMode
        /// </summary>
        public virtual int? GetMode
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MsgType
        /// </summary>
        public virtual int? MsgType
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RuleLevel
        /// </summary>
        public virtual int? RuleLevel
        {
            get; 
            set; 
        }        
		/// <summary>
		/// PushCount
        /// </summary>
        public virtual int? PushCount
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Content
        /// </summary>
        public virtual string Content
        {
            get; 
            set; 
        }        
		   
	}
}