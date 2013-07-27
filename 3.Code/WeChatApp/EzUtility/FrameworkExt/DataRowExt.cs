using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EzUtility.FrameworkExt
{
    public static class DataRowExt
    {
        /// <summary>
        /// 将DataRow转换成类型为T的实体对象，要求两者的属性名称要相同。  *扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow dr) where T : class
        {
            if (dr == null)
                return null;
            
            var properties = typeof(T).GetProperties();
            T info = Activator.CreateInstance<T>();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i] == null || !properties[i].CanWrite)
                    continue;

                if (dr.Table != null 
                    && dr.Table.Columns != null 
                    && dr.Table.Columns.Contains(properties[i].Name))
                {
                    var val = dr[properties[i].Name];
                    if (val != null && val != DBNull.Value)
                        properties[i].SetValue(info, val, null);
                }
            }

            return info;
        }

        public static T ToEntityByAttribute<T>(this DataRow dr) where T : class
        {
            if (dr == null)
                return null;

            var properties = typeof(T).GetProperties();

            T info = Activator.CreateInstance<T>();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i] == null || !properties[i].CanWrite)
                    continue;

                var att = properties[i].GetCustomAttributes(typeof(CustomAttribute.ColumnMapping), false);
                if (att != null && att.Length > 0)
                {                    
                    var mapping = att.First() as CustomAttribute.ColumnMapping;
                    if (mapping != null && !mapping.Name.IsNull())
                    {
                        if (dr.Table != null
                            && dr.Table.Columns != null
                            && dr.Table.Columns.Contains(mapping.Name))
                        {
                            var val = dr[mapping.Name];
                            if (val != null && val != DBNull.Value)
                                properties[i].SetValue(info, ConvertToMappingType(mapping.DataType, val), null);
                        }
                    }
                }
            }

            return info;
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
