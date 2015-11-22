using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.BLL;
using Maticsoft.Model;
using ExtendDLL;
using System.Text;
using System.Data;
using System.IO;


namespace lingxianWebservice
{
    /// <summary>
    /// getAlarm 获取某个报警数据的具体信息
    /// </summary>
    public class getAlarm : IHttpHandler
    {
        private static readonly Maticsoft.BLL.BaseSql exsql = new Maticsoft.BLL.BaseSql();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            string ZDBH = context.Request["ZDBH"];
            
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM (SELECT a.ZDBH,c.XMMC,c.UNIT,b.RELA,b.SETVA1,ISNULL(b.SETVA2,0) AS SETVA2,a.TRVA FROM ZB_BJJL AS a ");
            sb.Append(" INNER JOIN ZB_BJSET AS b ON a.ZDBH = b.ZDBH  AND a.SID=b.SID ");
            sb.Append(" INNER JOIN ZB_BJXM AS c ON b.XMID=c.XMID WHERE a.ZT='0' AND a.ZDBH='" + ZDBH + "' ) as t ");
            var points = exsql.getList(sb.ToString());



            if (null == points || points.Tables[0].Rows.Count == 0)
            {
                context.Response.Write("error:当前没有报警信息");
            }
            else
            {
                string callbackFunName = context.Request["callbackparam"];
                string str = callbackFunName + "(" + points.Tables[0].toJSON() + ")";
                context.Response.Write(str);
            }


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