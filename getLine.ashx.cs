using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.BLL;
using Maticsoft.Model;
using ExtendDLL;
using System.Text;
using System.Data;

namespace lingxianWebservice
{
    /// <summary>
    /// getLine 的摘要说明
    /// </summary>
    public class getLine : IHttpHandler
    {
        private static readonly Maticsoft.BLL.BaseSql bll = new Maticsoft.BLL.BaseSql();
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            
            string ZDID = context.Request.Form["ZDID"];
            string ZDBH = context.Request.Form["ZDBH"];
            string XSZT = context.Request.Form["XSZT"];
            string returnString = string.Empty;
            string strTableName = string.Empty;
            string strSql = string.Empty;
            StringBuilder sb=new StringBuilder();
            sb.Append("SELECT TOP 1 ");
            if (XSZT == "1")
            {
                strTableName = "ZB_Data_" + ZDBH;
                sb.Append(" CRSJ,SJ,[328] as lx_328,[329] as lx_329,[330] as lx_330,[331] as lx_331 FROM " + strTableName);


            }
            else if (XSZT == "2")
            {
                strTableName = "ZB_Data_" + ZDBH;
                sb.Append("CRSJ,SJ,[332] as lx_332,[333] as lx_333 FROM " + strTableName);
            }
            else
            {
                returnString = "error:错误状态";
            }
            sb.Append(" ORDER BY SJ DESC ");
            if (returnString.Length == 0)
            {
                DataSet ds = bll.getList(sb.ToString());

                if (null == ds)
                {
                    returnString = "error:查无数据";
                }
                else
                {
                    returnString = ds.Tables[0].toJSON();
                }
            }
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