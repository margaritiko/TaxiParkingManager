using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiParkingLibrary
{
    public class FIlter
    {
        public string AdmAreaValue { get; set; } = "";
    
        public uint MinCarCapacity { get; set; } = 0;
        public uint MaxCarCapacity { get; set; } = uint.MaxValue;
    }

}
