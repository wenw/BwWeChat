using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Newtonsoft.Json;

namespace EzUtility.FrameworkExt
{
    public static class ObjectExt
    {
        /// <summary>
        /// 安全转换成Int类型。 *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this object obj, int defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            int.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成Long类型。    *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this object obj, long defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            long.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成Float类型。   *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            float.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成Double类型。  *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            double.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成Decimal类型。 *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            decimal.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成String类型。  *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringExt(this object obj, string defaultValue = null)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            return obj.ToString();
        }

        /// <summary>
        /// 安全转换成Byte类型。    *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this object obj, byte defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            byte.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 安全转换成Sbyte类型。   *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this object obj, sbyte defaultValue = 0)
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            sbyte.TryParse(obj.ToString(), out defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// *扩展方法 => 安全转换成DateTime类型。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return DateTime.MinValue;

            DateTime val;
            DateTime.TryParse(obj.ToString(), out val);
            return val;
        }

        /// <summary>
        /// *扩展方法 => 安全转换成bool类型。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            bool val;
            bool.TryParse(obj.ToString(), out val);
            return val;
        }

        /// <summary>
        /// *扩展方法 => 安全转换成Guid类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return Guid.Empty;

            Guid id = Guid.Empty;
            Guid.TryParse(obj.ToString(), out id);
            return id;
        }

        /// <summary>
        /// 将对象进行二进制序列化。    *扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToBinarySerialization(this object obj)
        {
            if (obj.IsNull())
                return null;

            byte[] data = null;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                data = ms.ToArray();                
            }

            return data;
        }

        public static bool IsNull(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return true;
            else
                return false;
        }

        public static string ToJsonString(this object data)
        {
            if (data.IsNull())
                return null;

            return JsonConvert.SerializeObject(data);
        }
    }
}
