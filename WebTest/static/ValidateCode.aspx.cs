using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;

/// <summary>
/// ValidateCode 的摘要说明
/// </summary>
public partial class ValidateCode : Page, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = string.Empty;
        Color[] colorArray = new Color[] { Color.Green };
        string[] strArray = new string[] { "黑体" };
        char[] chArray = new char[] { 
            '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'E', 'F', 'G', 'H', 'J', 
            'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y'
         };
        Random random = new Random();
        for (int i = 0; i < 4; i++)
        {
            str = str + chArray[random.Next(chArray.Length)];
        }
        this.Session["CheckCode"] = str;
        Bitmap image = new Bitmap(100, 40);
        Graphics graphics = Graphics.FromImage(image);
        graphics.Clear(Color.White);
        for (int j = 0; j < str.Length; j++)
        {
            string familyName = strArray[random.Next(strArray.Length)];
            Font font = new Font(familyName, 18f);
            Color color = colorArray[random.Next(colorArray.Length)];
            graphics.DrawString(str[j].ToString(), font, new SolidBrush(color), (float)((j * 20f) + 8f), (float)8f);
        }
        base.Response.Buffer = true;
        base.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(0.0);
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        base.Response.AppendHeader("Pragma", "No-Cache");
        MemoryStream stream = new MemoryStream();
        try
        {
            image.Save(stream, ImageFormat.Png);
            base.Response.ClearContent();
            base.Response.ContentType = "image/Png";
            base.Response.BinaryWrite(stream.ToArray());
        }
        finally
        {
            image.Dispose();
            graphics.Dispose();
        }
    }

}