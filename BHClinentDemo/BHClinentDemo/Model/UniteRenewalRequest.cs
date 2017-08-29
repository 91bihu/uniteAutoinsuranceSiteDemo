using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BHClinentDemo.Model
{
   public class UniteRenewalRequest
    {
      
        public string LicenseNo { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        [Required]
        public int CityCode { get; set; }
       
        /// <summary>
        /// 车架号(必须是17位)
        /// </summary>
        public string CarVin { get; set; }

 
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNo { get; set; }
        public string AgentId { get; set; }
        public string SecCode { get; set; }
        public string Token { get; set; }
        public string SecretKey { get; set; }
        public long TimeStamp { get; set; }

    }
}
