using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konasoft.models
{
    public class Country
    {
        public string CallingCode { get; set; }
        public string Name { get; set; }

        public Country(string callingCode, string name)
        {
            this.CallingCode = callingCode;
            this.Name = name;
        }


    }
}
