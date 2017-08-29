using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BHClinentDemo.Model
{
    /// <summary>
    /// 联合登录请求Model
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
}
