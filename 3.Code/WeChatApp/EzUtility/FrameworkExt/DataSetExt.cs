using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EzUtility.FrameworkExt
{
    public static class DataSetExt
    {
        /// <summary>
        /// 判断是否存在可用的DataTable。 *扩展方法
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool HasTable(this DataSet ds)
        {
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 返回DataSet中第一个DataTable。  *扩展方法
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataTable GetFirstTable(this DataSet ds)
        {
            if (!HasTable(ds))
                return null;

            return ds.Tables[0];
        }

        /// <summary>
        /// 判断是否有可用的记录。 *扩展方法
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool HasRecord(this DataSet ds)
        {
            if (!HasTable(ds))
                return false;

            var dt = GetFirstTable(ds);

            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return false;
            else
                return true;
        }
    }
}
