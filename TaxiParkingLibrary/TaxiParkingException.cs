using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaxiParkingLibrary
{
    [Serializable]
    public class TaxiParkingException : Exception
    {
        public TaxiParkingException() { }

        public TaxiParkingException(string message) : base(message) { }

        public TaxiParkingException(string message, Exception innerException) : base(message, innerException) { }

        protected TaxiParkingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    
}
