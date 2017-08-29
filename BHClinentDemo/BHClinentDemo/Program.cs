using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BHClinentDemo
{
    class Program
    {
      
        static void Main(string[] args)
        {

            //联合登录
           // BHClient.UnitLogin();
            //续保接口
             BHClient.Renewal("467E1F4CBDE50D1885CDF9E2DB02EE45");
            Console.ReadKey();
        }
    }
}
