using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.Domain
{
	 	//MPUserInfo
		public class MPUserInfo
	{
            public MPUserInfo()
            {
                AccountId = 0;
                FakeId = string.Empty;
                OpenId = string.Empty;
                NickName = string.Empty;
                RemarkName = string.Empty;
                MobileNumber = string.Empty;
                QQNumber = string.Empty;
                IDCard = string.Empty;
                BirthdayY = 0;
                BirthdayM = 0;
                BirthdayD = 0;
                Sex = 0;
                Country = string.Empty;
                Province = string.Empty;
                City = string.Empty;
                MPLanguage = string.Empty;
                HeadImgUrl = string.Empty;
                GroupId = string.Empty;
                WeBackGroupId = string.Empty;
                SubscribeTime = 0;
                CreateTime = DateTime.Now;
                LastVisitTime = DateTime.Now;
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
        public virtual int AccountId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// FakeId
        /// </summary>
        public virtual string FakeId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// OpenId
        /// </summary>
        public virtual string OpenId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// NickName
        /// </summary>
        public virtual string NickName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RemarkName
        /// </summary>
        public virtual string RemarkName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MobileNumber
        /// </summary>
        public virtual string MobileNumber
        {
            get; 
            set; 
        }        
		/// <summary>
		/// QQNumber
        /// </summary>
        public virtual string QQNumber
        {
            get; 
            set; 
        }        
		/// <summary>
		/// IDCard
        /// </summary>
        public virtual string IDCard
        {
            get; 
            set; 
        }        
		/// <summary>
		/// BirthdayY
        /// </summary>
        public virtual int BirthdayY
        {
            get; 
            set; 
        }        
		/// <summary>
		/// BirthdayM
        /// </summary>
        public virtual int BirthdayM
        {
            get; 
            set; 
        }        
		/// <summary>
		/// BirthdayD
        /// </summary>
        public virtual int BirthdayD
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Sex
        /// </summary>
        public virtual int Sex
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Country
        /// </summary>
        public virtual string Country
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Province
        /// </summary>
        public virtual string Province
        {
            get; 
            set; 
        }        
		/// <summary>
		/// City
        /// </summary>
        public virtual string City
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MPLanguage
        /// </summary>
        public virtual string MPLanguage
        {
            get; 
            set; 
        }        
		/// <summary>
		/// HeadImgUrl
        /// </summary>
        public virtual string HeadImgUrl
        {
            get; 
            set; 
        }        
		/// <summary>
		/// GroupId
        /// </summary>
        public virtual string GroupId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// WeBackGroupId
        /// </summary>
        public virtual string WeBackGroupId
        {
            get; 
            set; 
        }        
		/// <summary>
		/// SubscribeTime
        /// </summary>
        public virtual long SubscribeTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateTime
        /// </summary>
        public virtual DateTime CreateTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// LastVisitTime
        /// </summary>
        public virtual DateTime LastVisitTime
        {
            get; 
            set; 
        }        
		   
	}
}