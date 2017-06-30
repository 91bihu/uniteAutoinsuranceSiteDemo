using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BHClinentDemo
{
   public class BHClient
    {
      // private readonly static Uri url = new Uri("http://lzl.91bihu.com/");

       public static void UnitLogin()
       {
           UniteLoginRequest request = new UniteLoginRequest() {

               agentId = 73663,
               userName="内测",
               timestamp = DateTime.Now.ConvertToTimeStmap(),
               timeout=5000
           };
           request.secCode = request.ToSecCode("60a78c69d89");

           string body = JsonConvert.SerializeObject(request);

           var response = PostResponse("http://bao.91bihu.com/Unite/Login", body);
           if (response.Item1)
           {
               Console.WriteLine(response.Item2);
           }
           else
           {
               Console.WriteLine("请求不成功");
           }

       }


       public static Tuple<bool, string> PostResponse(string url, string body)
       {
           try
           {
               using (HttpClient client = new HttpClient(new HttpClientHandler()))
               {
                   HttpContent content = new StringContent(body);
                   MediaTypeHeaderValue typeHeader = new MediaTypeHeaderValue("application/json");
                   typeHeader.CharSet = "UTF-8";
                   content.Headers.ContentType = typeHeader;

                   var res = client.PostAsync(url, content, new CancellationTokenSource().Token).Result;
                 
                   if (res.IsSuccessStatusCode)
                   {
                       string result = res.Content.ReadAsStringAsync().Result;

                       if (res.StatusCode == HttpStatusCode.OK)
                       {
                           return Tuple.Create(true, result);
                       }

                   }

                   return Tuple.Create(false, res.StatusCode.ToString());
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return Tuple.Create(false, "");
       }

      

      
    }

   public class UniteLoginRequest
   {
       public int agentId { get; set; }
       public string userName { get; set; }
       public long timestamp { get; set; }
       public string secCode { get; set; }
       public int timeout { get; set; }
   }

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

       public static string ToSecCode(this UniteLoginRequest request, string secretKey)
       {
           Dictionary<string, string> dic = new Dictionary<string, string>();
           dic.Add("agentId", request.agentId.ToString());
           dic.Add("userName", request.userName);
           dic.Add("secretKey", secretKey);
           dic.Add("timestamp", request.timestamp.ToString());
           dic.Add("timeout", request.timeout.ToString());
           string _str = string.Join("&", dic.Select(p => p.Key + '=' + p.Value).ToArray());
           string testMd5 = "agentId=77765&userName=内测&secretKey=80c9e681654&timestamp=1492697401&timeout=5000".ToMD5();
           return string.Join("&", dic.Select(p => p.Key + '=' + p.Value).ToArray()).ToMD5();
       }

   }
    
}
