using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BHClinentDemo.Common;
using BHClinentDemo.Model;

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
        private const string BASEURL = "http://bao.91bihu.com/";

        private const int TOPAGENTID = 1222;//顶级ID
        private const string SECRETKEY = "";// 密钥商务合作后由壁虎提供

        public static void UnitLogin()
        {
            UniteLoginRequest request = new UniteLoginRequest()
            {
                agentId = TOPAGENTID,
                userName = "test",
                timestamp = DateTime.Now.ConvertToTimeStmap(),
                timeout = 50000
            };
            request.secCode = request.ToSecCode(SECRETKEY);

            string body = JsonConvert.SerializeObject(request);

            var response = PostResponse(string.Concat(BASEURL, "Unite/Login"), body);
            if (response.Item1)
            {
                Console.WriteLine(response.Item2);
            }
            else
            {
                Console.WriteLine("请求不成功");
            }
        }

        /// <summary>
        /// 退出接口
        /// </summary>
        /// <param name="_token">联合登陆返回的token</param>
        public static void Logout(string _token)
        {
            var request = new UniteLogOutRequest
            {
                agentId = TOPAGENTID,
                //联合登录返回的token
                token = _token,
            };
            var dic = request.ToDictionary();

            string body = JsonConvert.SerializeObject(dic.AttachSecCode(SECRETKEY));

            var response = PostResponse("http://bao.91bihu.com/unite/SignOut", body);
            if (response.Item1)
            {
                Console.WriteLine(response.Item2);
            }
            else
            {
                Console.WriteLine("请求不成功");
            }
        }

        /// <summary>
        /// 续保接口
        /// </summary>
        /// <param name="token">联合登录返回的token</param>
        public static void Renewal(string token)
        {
            UniteRenewalRequest request = new UniteRenewalRequest()
            {
                 CarVin = "LGXC16AF990213092",
                 EngineNo = "4G15S 4L9AM4623",
                 LicenseNo = "粤A599YK",
                CityCode = 14,
                AgentId = TOPAGENTID.ToString(),
                Token = token,
                TimeStamp = DateTime.Now.ConvertToTimeStmap()
            };
            var dic = request.ToDictionary();
            string body = JsonConvert.SerializeObject(dic.AttachSecCode(SECRETKEY));

            var response = PostResponse(string.Concat(BASEURL,"unite/unitecheckrenewal"), body);
            if (response.Item1)
            {
                Console.WriteLine(response.Item2);
            }
            else
            {
                Console.WriteLine(response.Item2);
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
   
}