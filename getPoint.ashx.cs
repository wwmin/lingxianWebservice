using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.BLL;
using Maticsoft.Model;
using ExtendDLL;
namespace lingxianWebservice
{
    /// <summary>
    /// getPoint 的摘要说明
    /// </summary>
    public class getPoint : IHttpHandler
    {
        private static readonly Maticsoft.BLL.ZB_Data_all_one bll = new Maticsoft.BLL.ZB_Data_all_one();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string returnString = string.Empty;
            string ZDID = context.Request.Form["ZDID"];
            string ZDBH = context.Request.Form["ZDBH"];
            returnString = bll.GetList("SBH='" + ZDBH + "'").Tables[0].toJSON();




            context.Response.Write(returnString);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}