using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EzUtility.FrameworkExt
{
    public static class DataTableExt
    {
        /// <summary>
        /// 判断是否存在可用的记录。    *扩展方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool HasRecords(this DataTable dt)
        {
            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 返回DataTable中第一行的DataRow。    *扩展方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataRow GetFirstRow(this DataTable dt)
        {
            if (!HasRecords(dt))
                return null;
            else
                return dt.Rows[0];
        }

        /// <summary>
        /// 将DataTable中的数据转换成类型为T的实体对象集合，要求两者的属性名称要相同。  *扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToEntities<T>(this DataTable dt) where T : class
        {
            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return null;

            List<T> dataList = Activator.CreateInstance<List<T>>();

            var members = typeof(T).GetProperties();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                T info = Activator.CreateInstance<T>();

                for (int i = 0; i < members.Length; i++)
                {
                    var property = typeof(T).GetProperty(members[i].Name);

                    if (dt.Columns != null && dt.Columns.Contains(property.Name))
                    {
                        var val = dt.Rows[j][property.Name];
                        if (val != null && val != DBNull.Value && property != null && property.CanWrite)
                            property.SetValue(info, val, null);
                    }
                }

                dataList.Add(info);
            }

            return dataList;
        }

        public static List<T> ToEntitiesByAttribute<T>(this DataTable dt) where T: class
        {
            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return null;

            List<T> dataList = Activator.CreateInstance<List<T>>();

            var members = typeof(T).GetProperties();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                T info = Activator.CreateInstance<T>();

                for (int i = 0; i < members.Length; i++)
                {
                    if (members[i] == null || !members[i].CanWrite)
                        continue;

                    var att = members[i].GetCustomAttributes(typeof(CustomAttribute.ColumnMapping), false);
                    if (att != null && att.Length > 0)
                    {
                        var mapping = att.First() as CustomAttribute.ColumnMapping;
                        if (mapping != null || !mapping.Name.IsNull())
                        {
                            if (dt.Columns != null && dt.Columns.Contains(mapping.Name))
                            {
                                var val = dt.Rows[j][mapping.Name];
                                if (val != null && val != DBNull.Value)
                                {  
                                    
                                    members[i].SetValue(info, ConvertToMappingType(mapping.DataType, val), null);
                                }
                            }
                        }
                    }
                }

                dataList.Add(info);
            }

            return dataList;
        }

        public static object ConvertToMappingType(DbType type, object val)
        {
            object newData = null;
            switch (type)
            {
                case DbType.Byte:
                    newData = val.ToByte();
                    break;
                case DbType.Boolean:
                    newData = val.ToBool();
                    break;
                case DbType.DateTime:
                    newData = val.ToDateTime();
                    break;
                case DbType.Decimal:
                    newData = val.ToDecimal();
                    break;
                case DbType.Guid:
                    newData = val.ToGuid();
                    break;
                case DbType.Double:
                    newData = val.ToDouble();
                    break;
                case DbType.Int32:
                    newData = val.ToInt();
                    break;
                case DbType.Int64:
                    newData = val.ToLong();
                    break;
                case DbType.String:
                    newData = val.ToStringExt();
                    break;                
                default:
                    newData = val;
                    break;
            }

            return newData;
        }
    }
}
