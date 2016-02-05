using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public class SystemMessage
    {
        public String id { get; set; }
        public String key { get; set; }
        public String name { get; set; }
    }

    public struct IP
    {
        public String ip { get; set; }
    }
}
