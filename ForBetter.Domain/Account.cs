using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Domain
{
    public class Account
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>

        public virtual string AccountUserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public virtual string AccountPassword { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime AccountJoinTime { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime AccountLastLogin { get; set; }
        /// <summary>
        /// 用户是否禁止登录
        /// </summary>
        public virtual int IsStop { get; set; }

        /// <summary>
        /// 所属代理商
        /// </summary>
        public virtual int AgentId { get; set; }
        /// <summary>
        /// 代理商写入的备注
        /// </summary>

        public virtual string AgentMemo { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>

        public virtual string AccountEmail { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>

        public virtual string AccountMobile { get; set; }
    }
}
