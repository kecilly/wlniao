using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wlniao.Model
{
    public class MiniSite
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string MsgTitle { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        public string MPAccount { get; set; }
        /// <summary>
        /// LOGO地址
        /// </summary>
        public string LogoSrc { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 主题颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 底部版权
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// 迷你导航风格：0为不使用
        /// </summary>
        public int MiniNav { get; set; }

    }
    public class MiniSiteStyle
    {
        public string StylePath { get; set; }
        public string StyleName { get; set; }
        public string PicPath { get; set; }

        public bool Logo { get; set; }
        public bool Banner { get; set; }
        public bool Color { get; set; }
        public bool MiniNav { get; set; }
        public bool CopyRight { get; set; }
    }
    public class MiniSiteImgLink
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
    }
    public class MiniNavLink
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Src { get; set; }
    }
}
