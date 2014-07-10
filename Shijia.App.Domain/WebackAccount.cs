using System;

//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Shijia.App.Domain
{
    public class WebackAccount
    {

        /// <summary>
        /// ID
        /// </summary>
        public virtual decimal ID
        {
            get;
            set;
        }
        /// <summary>
        /// AppUserId
        /// </summary>
        public virtual string AppUserId
        {
            get;
            set;
        }
        /// <summary>
        /// MPToken
        /// </summary>
        public virtual string MPToken
        {
            get;
            set;
        }
        /// <summary>
        /// AppToken
        /// </summary>
        public virtual string AppToken
        {
            get;
            set;
        }
        /// <summary>
        /// WebackUrl
        /// </summary>
        public virtual string WebackUrl
        {
            get;
            set;
        }
        /// <summary>
        /// Period
        /// </summary>
        public virtual string Period
        {
            get;
            set;
        }

    }
}