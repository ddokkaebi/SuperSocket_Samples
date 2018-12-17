using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServerLib
{
    public class Util
    {
        const Int64 TicksPerMicrosecond = 10;

        public static Int64 TimeTickToSec(Int64 curTimeTick)
        {
            Int64 sec = (Int64)(curTimeTick / TimeSpan.TicksPerSecond);
            return sec;
        }

        public static Int64 TimeTickToNanoSec(Int64 curTimeTick)
        {
            Int64 sec = (Int64)(curTimeTick / TicksPerMicrosecond);
            return sec;
        }

        public static string ToJsonFormatString<T>(T inputData)
        {
            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(inputData);
            return jsonstring;
        }

        public static T JsonFormatStringToObject<T>(string jsonFormat)
        {
            var objectT = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonFormat);
            return objectT;
        }
    }
}
