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
    /// getAllPoints 的摘要说明
    /// </summary>
    public class getAllPoints : IHttpHandler
    {
        private static readonly Maticsoft.BLL.ZB_ZDXX bll = new Maticsoft.BLL.ZB_ZDXX();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string returnString = string.Empty;

            returnString = bll.GetAllList().Tables[0].toJSON();




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