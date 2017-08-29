using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BHClinentDemo.Model;

namespace BHClinentDemo.Common
{
   public static class Uititly
    {
       public static Dictionary<string, string> AttachSecCode(this Dictionary<string, string> dic,string secretKey)
       {
           dic.Add("secretKey", secretKey);
           var tmp = dic.Where(x => !string.IsNullOrWhiteSpace(x.Value)).OrderBy(y => y.Key);
           StringBuilder data = new StringBuilder();
           string result = string.Join("&", tmp.Select(p => p.Key + '=' + p.Value.Trim()).ToArray().OrderBy(x => x));
           dic.Add("secCode", result.ToMD5().ToUpper());
           dic.Remove("secretKey");
           return dic;
       }

       public static string ToSecCode( this UniteLoginRequest request, string secretKey)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("agentId", request.agentId.ToString());
           dic.Add("userName", request.userName);
           dic.Add("secretKey", secretKey);
           dic.Add("timestamp", request.timestamp.ToString());
           dic.Add("timeout", request.timeout.ToString());
           return string.Join("&", dic.Select(p => p.Key + '=' + p.Value).ToArray()).ToMD5();
       }
    }
}
