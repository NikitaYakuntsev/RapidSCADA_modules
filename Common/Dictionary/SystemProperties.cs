using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Dictionary
{
    public class SystemProperties
    {
        public static string SP_SCADA_NAME = "SCADA_NAME";
        public static string SP_SCADA_ID = "SCADA_ID";
        public static string SP_CLOUD_HOST = "CLOUD_HOST";
        public static string SP_REGISTERED = "REGISTERED_IN_CLOUD";

        public static string EP_REGISTER = "SCADA/Register";
        public static string EP_HEARTBEAT = "SCADA/Hearthbeat";
    }
}
