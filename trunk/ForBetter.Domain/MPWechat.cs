using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Domain
{
    public class MPWechat
    {
        public MPWechat()
        {
            MPType = 0;
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
        /// WeixinName
        /// </summary>
        public virtual string WeixinName
        {
            get;
            set;
        }
        /// <summary>
        /// Token
        /// </summary>
        public virtual string Token
        {
            get;
            set;
        }
        /// <summary>
        /// FristAccount
        /// </summary>
        public virtual string FristAccount
        {
            get;
            set;
        }
        /// <summary>
        /// ManagerOpenid
        /// </summary>
        public virtual string ManagerOpenid
        {
            get;
            set;
        }
        /// <summary>
        /// VerifyContent
        /// </summary>
        public virtual string VerifyContent
        {
            get;
            set;
        }
        /// <summary>
        /// WelcomeMsg
        /// </summary>
        public virtual string WelcomeMsg
        {
            get;
            set;
        }

        public virtual int WelcomeMPType
        {
            get;
            set;
        }
        /// <summary>
        /// DefaultMsg
        /// </summary>
        public virtual string DefaultMsg
        {
            get;
            set;
        }
        /// <summary>
        /// MPType
        /// </summary>
        public virtual int DefaultMPType
        {
            get;
            set;
        }

        public virtual int MPType
        {
            get;
            set;
        }

        /// <summary>
        /// AppId
        /// </summary>
        public virtual string AppId
        {
            get;
            set;
        }
        /// <summary>
        /// KeyWordsList
        /// </summary>
        public virtual string KeyWordsList
        {
            get;
            set;
        }
        /// <summary>
        /// AppSecret
        /// </summary>
        public virtual string AppSecret
        {
            get;
            set;
        }

    }
}
