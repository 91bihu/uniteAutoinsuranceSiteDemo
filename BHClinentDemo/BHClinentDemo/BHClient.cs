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

/**
 * 
 * 代码段为内嵌壁虎车险机器人后台示例程序
 * 
 * TOPAGENTID 值为示例值
 * 
 * SECRETKEY 为示例值
 * 
 * 总体按照商家自己的情况而定
 */
namespace BHClinentDemo
{
   public class BHClient
    {
 
       /// <summary>
       /// 联合登录接口地址
       /// </summary>
       private const string UNITEURL = "http://bao.91bihu.com/Unite/Login";
       private const int TOPAGENTID=123456;//顶级ID
       private const string SECRETKEY="@#$%^&*I$%&";// 密钥商务合作后由壁虎提供

       public static void UnitLogin()
       {
           UniteLoginRequest request = new UniteLoginRequest() {

               agentId = TOPAGENTID,
               userName="测试",
               timestamp = DateTime.Now.ConvertToTimeStmap(),
               timeout=5000
           };
           request.secCode = request.ToSecCode(SECRETKEY); 

           string body = JsonConvert.SerializeObject(request);

           var response = PostResponse(UNITEURL, body);
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

    /// <summary>
    /// 联合登录请求对象
    /// </summary>

   public class UniteLoginRequest
   {
       /// <summary>
       /// 顶级ID
       /// </summary>
       public int agentId { get; set; }
       /// <summary>
       /// 合作方登录用户名
       /// </summary>
       public string userName { get; set; }
       /// <summary>
       /// 时间戳
       /// </summary>
       public long timestamp { get; set; }
       /// <summary>
       /// 加密串
       /// </summary>
       public string secCode { get; set; }
       /// <summary>
       /// 登录过期时间
       /// </summary>
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
         
           return string.Join("&", dic.Select(p => p.Key + '=' + p.Value).ToArray()).ToMD5();
       }

   }
    
}
