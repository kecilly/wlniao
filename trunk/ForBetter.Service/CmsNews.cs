using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.Model
{
    public class CmsNews
    {
        /// <summary>
        /// Index
        /// </summary>
        public string NewsIndex { get; set; }
        /// <summary>
        /// 栏目标题
        /// </summary>
        public string ClassTitle { get; set; }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string NewsTitle { get; set; }
        /// <summary>
        /// 简要
        /// </summary>
        public string ShortContent { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 数据文件的Guid
        /// </summary>
        public string GuidContent { get; set; }
        /// <summary>
        /// 外链地址
        /// </summary>
        public string NewsUrl { get; set; }
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public bool ShowInHomePage { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string NewsIcons { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 首页文件
        /// </summary>
        public string HomePageFile { get; set; }
    }
}
