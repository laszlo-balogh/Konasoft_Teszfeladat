using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konasoft.models
{
    public class OutputData
    {

        public string CountryName { get; set; }
        public string AreaName { get; set; }
        public string CountryCallingCode { get; set; }
        public string AreaCallingCode { get; set; }
        public int SumCallingDuration { get; set; }

        public OutputData(string countryName, string areaName, string countryCallingCode, string areaCallingCode, int sumCallingDuration)
        {
            this.CountryName = countryName;
            this.AreaName = areaName;
            this.CountryCallingCode = countryCallingCode;
            this.AreaCallingCode = areaCallingCode;
            this.SumCallingDuration = sumCallingDuration;
        }

        public override string ToString()
        {
            return $"{this.CountryName}\t{this.AreaName}\t{this.CountryCallingCode}\t{this.AreaCallingCode}\t{this.SumCallingDuration}";
        }
    }
}
