using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.Domain
{
	 	//MPMenu
		public class MPMenu
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
		/// AccountId
        /// </summary>
        public virtual int? AccountId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MPTreeData
        /// </summary>
        public virtual string MPTreeData
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MPMenuData
        /// </summary>
        public virtual string MPMenuData
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MPMenuSequense
        /// </summary>
        public virtual int? MPMenuSequense
        {
            get; 
            set; 
        }        
		   
	}
}