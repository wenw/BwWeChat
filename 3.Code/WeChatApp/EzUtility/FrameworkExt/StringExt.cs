using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EzUtility.FrameworkExt
{
    public static class StringExt
    {
        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string ToBase64(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string FromBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }

        public static T ToObjectFromJson<T>(this string json)
        {
            if (json.IsNull())
                return default(T);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToDesEncrypt(this string data, string key)
        {
            string result = null;
            byte[] iv = new byte[] { 2, 0, 1, 3, 0, 2, 2, 0, };
            byte[] keyArray = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] dataArray = Encoding.UTF8.GetBytes(data);
            
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            {   
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(keyArray, iv), CryptoStreamMode.Write);
                cStream.Write(dataArray, 0, dataArray.Length);
                cStream.FlushFinalBlock();
                result = Convert.ToBase64String(mStream.ToArray());
            }

            return result;
        }

        public static string ToDesDecrypt(this string encryptData, string key)
        {
            string result = null;
            byte[] iv = new byte[] { 2, 0, 1, 3, 0, 2, 2, 0, };
            byte[] keyArray = Encoding.UTF8.GetBytes(key.Substring(0, 8));

            byte[] bStr = Convert.FromBase64String(encryptData);
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            {
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateDecryptor(keyArray, iv), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                result = Encoding.UTF8.GetString(mStream.ToArray());
            }
            return result;
        }
    }
}
