using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Threading;
using Shijia.Domain;
namespace Wlniao
{
    /// <summary>
    /// 分发
    /// </summary>
    public class Hub
    {
        public class HubThread
        {
            /// <summary>
            /// 标识
            /// </summary>
            public String Guid { get; set; }
            /// <summary>
            /// 输入
            /// </summary>
            public String Input { get; set; }
            /// <summary>
            /// 输出
            /// </summary>
            public String Output { get; set; }
            /// <summary>
            /// 公众帐号
            /// </summary>
            public MPWechat MPWechat { get; set; }
            /// <summary>
            /// 当前请求
            /// </summary>
            public HttpContext ctx { get; set; }

            /// <summary>
            /// 当前请求URL
            /// </summary>
            public string APIUrl { get; set; }
            /// <summary>
            /// 时间刻度
            /// </summary>
            public long Tick { get; set; }

            public Spring.Context.IApplicationContext IAC { get; set; }
        }
        internal static Dictionary<String, HubThread> Stack = new Dictionary<String, HubThread>();


        internal static void Start(MPWechat wechat, string  Input, out String output,Spring.Context.IApplicationContext IAC,string APIUrl)
        {
            output = string.Empty;
            HubThread thread = new HubThread();
            thread.Guid = System.Guid.NewGuid().ToString();
            try
            {
                thread.Tick = DateTools.ConvertToUnixofLong();
                thread.Input = Input;
                thread.MPWechat = wechat;
                thread.ctx = HttpContext.Current;
                thread.IAC = IAC;
                thread.APIUrl = APIUrl;
                Stack.Add(thread.Guid, thread);
                new Thread(new ParameterizedThreadStart(HubMethod.Default.Do)).Start(thread.Guid);  //默认流程

                for (int i = 0; i < 3000; i = i + 50)
                {
                    try
                    {
                        output = Stack[thread.Guid].Output;
                        if (!string.IsNullOrEmpty(output))
                        {
                            break;
                        }
                    }
                    catch { }
                    System.Threading.Thread.Sleep(47);
                }
            }
            catch { }
            Stack.Remove(thread.Guid);
        }



    }
}
