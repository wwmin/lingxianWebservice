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
    /// getAllAlarm 获取全部报警信息记录
    /// </summary>
    public class getAllAlarm : IHttpHandler
    {
        private static readonly Maticsoft.BLL.ZB_ZDXX bll = new Maticsoft.BLL.ZB_ZDXX();
        private static readonly Maticsoft.BLL.BaseSql exsql = new Maticsoft.BLL.BaseSql();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strsql = string.Empty;
            #region 获取报警点信息
            var points = exsql.getList("SELECT ZDBH FROM ZB_BJJL GROUP BY ZDBH");


            if (null == points || points.Tables[0].Rows.Count == 0)
            {
                 context.Response.Write("error:当前没有报警信息"); 
            }
            else
            {
                foreach (DataRow item in points.Tables[0].Rows)
                {
                    strsql += ",'" + item["ZDBH"].ToString() + "'";
                }
                strsql = strsql.Substring(1);
                strsql = " ZDBH IN (" + strsql + ")";
            }

            #endregion


            string returnString = string.Empty;


            string callbackFunName = context.Request["callbackparam"];

            returnString = bll.GetList(strsql).Tables[0].toJSON();

            string str = callbackFunName + "(" + returnString + ")";

            context.Response.Write(str);
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