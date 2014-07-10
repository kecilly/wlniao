using System;
using System.Collections.Generic;
using System.Web;

namespace Shijia.Service
{

    /// <summary>
    /// button 空接口，继承自button 会导致json序列化的时候 父类的字段在最后输出。就不采用继承字段，此接口只代表一种约束
    /// </summary>
    public interface WXMenuIButton
    {
    }

    /// <summary>
    /// 本地的按钮
    /// </summary>
    internal class WXMenuButton : WXMenuIButton
    {
        public WXMenuButton()
        {
            this.children = new List<WXMenuButton>();
        }
        public string text { get; set; }
        public string type { get; set; }
        public string key { get; set; }
        public List<WXMenuButton> children { get; set; }
    }
    /// <summary>
    /// 普通按钮
    /// </summary>
    internal class WXMenuCommonButton : WXMenuIButton
    {
        public WXMenuCommonButton()
        {
            this.type = "click";
        }
        public string name { get; set; }
        public string type { get; set; }
        public string key { get; set; }
        public string url { get; set; }
    }

    /// <summary>
    /// 包含子按钮的按钮
    /// </summary>
    internal class WXMenuComplexButton : WXMenuIButton
    {
        public WXMenuComplexButton()
        {
            this.sub_button = new List<WXMenuCommonButton>();
        }
        public string name { get; set; }
        public List<WXMenuCommonButton> sub_button { get; set; }
    }
    /// <summary>
    /// 菜单
    /// </summary>
    public class WXMenu
    {
        public WXMenu()
        {
            this.button = new List<WXMenuIButton>();
        }
        public List<WXMenuIButton> button { get; set; }
    }
}