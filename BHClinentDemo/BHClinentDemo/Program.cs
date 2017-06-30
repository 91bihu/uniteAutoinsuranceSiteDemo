using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BHClinentDemo
{
    class Program
    {
        /// <summary>
        /// 发动机号
        /// </summary>
        public const string PatEngineNo = @"^[a-zA-Z0-9]*[-\.\*\s]*[a-zA-Z0-9\*]+$";
        static void Main(string[] args)
        {
            Regex regex = new Regex(PatEngineNo, RegexOptions.Compiled);
            bool s= regex.IsMatch("LE5 091700137 ");

           // BHClient.UnitLogin();
            Console.ReadKey();
        }
    }
}
