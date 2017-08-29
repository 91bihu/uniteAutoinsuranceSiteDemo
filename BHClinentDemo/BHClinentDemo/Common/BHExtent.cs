using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BHClinentDemo.Common
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class BHExtent
    {
        public static long ConvertToTimeStmap(this DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        public static string ToMD5(this string s)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            byte[] hash = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(s));

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
