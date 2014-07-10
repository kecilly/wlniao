using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.Model
{
    public class CmsClass
    {
        /// <summary>
        /// Index
        /// </summary>
        public string ClassIndex { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassTitle { get; set; }
        /// <summary>
        /// 栏目内容
        /// </summary>
        public string ClassContent { get; set; }
        /// <summary>
        /// 数据文件的Guid
        /// </summary>
        public string GuidContent { get; set; }
        /// <summary>
        /// 栏目类型
        /// </summary>
        public string ClassType { get; set; }
        /// <summary>
        /// 栏目图标
        /// </summary>
        public string ClassIcons { get; set; }
        /// <summary>
        /// 栏目外链
        /// </summary>
        public string ClassUrl { get; set; }
        /// <summary>
        /// 栏目排序
        /// </summary>
        public string ClassSort { get; set; }
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public bool ShowInHomePage { get; set; }
        /// <summary>
        /// 是否在导航条显示
        /// </summary>
        public bool ShowInNavBar { get; set; }


        public static int CompareBySort(Wlniao.Model.CmsClass cmsclass1, Wlniao.Model.CmsClass cmsclass2)
        {
            return String.Compare(cmsclass1.ClassSort, cmsclass2.ClassSort);
        }
        public static int CompareBySortDesc(Wlniao.Model.CmsClass cmsclass1, Wlniao.Model.CmsClass cmsclass2)
        {
            return String.Compare(cmsclass2.ClassSort, cmsclass1.ClassSort);
        }
    }
}
