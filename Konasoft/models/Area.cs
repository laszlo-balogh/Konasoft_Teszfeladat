using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konasoft.models
{
    public class Area

    {
        public string CountryCallingCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Area(string countryCallingCode, string code, string name)
        {
            this.CountryCallingCode = countryCallingCode;
            this.Code = code;
            this.Name = name;
        }



    }
}
