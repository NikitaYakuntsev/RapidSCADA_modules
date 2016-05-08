using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utils
    {
        public static long GetUnixTime()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static String GetJsonToken(string input, string parentToken)
        {
            JObject obj = JObject.Parse(input);
            return obj.SelectToken(parentToken).ToString();
        }

    }
}
