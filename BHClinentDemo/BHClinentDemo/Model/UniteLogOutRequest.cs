using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BHClinentDemo.Model
{
    /// <summary>
    /// 联合退出接口的请求Model
    /// </summary>
    public class UniteLogOutRequest
    {
        public int agentId { get; set; }

        public string token { get; set; }

        public string secCode { get; set; }
    }
}
