using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace ExtendDLL
{
    public static class ExtendDLL
    {
        #region 扩展string    字符串方法
        /// <summary>
        /// 将字符串转换为数值 sndddx 20150803
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int toInt(this string s)
        {
            int i;
            if (!int.TryParse(s, out i))
            {
                i = 0;
            }
            return i;
        }
        /// <summary>
        /// 将所有对象转为字符串  sndddx 20150803
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string toStr(this object s, string format = "")
        {
            string result = "";
            try
            {
                if (format == "")
                {
                    result = s.ToString();
                }
                else
                {
                    result = string.Format("{0:" + format + "}", s);
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion


        #region 扩展int       数值方法

        #endregion


        #region 扩展DataTable    字符串方法
        /// <summary>
        /// DataTablel数据转换为JSON字符串，并不是JSON对象  sndddx 20150803
        /// </summary>
        /// <param name="pDataTable"></param>
        /// <returns></returns>
        public static string toJSON(this DataTable pDataTable)
        {
            if (pDataTable == null || pDataTable.Columns.Count == 0 || pDataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = new ArrayList();
                foreach (DataRow dataRow in pDataTable.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                    foreach (DataColumn dataColumn in pDataTable.Columns)
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].toStr());
                    }
                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                string str= javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
                //if (str.Substring(0, 1) == "[")
                //{
                //    str = str.Substring(1);
                //    str = str.Substring(0, str.Length - 1);
                //}
                return str;
            }
        }

        /// <summary>
        /// IList数据转DataTable sndddx 20150803
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable toDataTable<T>(this IList<T> list)
        {
            Type elementType = typeof(T);

            var t = new DataTable();

            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return t;
        }

        #endregion

    }
}
