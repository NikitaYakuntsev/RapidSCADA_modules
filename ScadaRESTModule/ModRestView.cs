using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scada.Data;

namespace Scada.Server.Modules
{
    class ModRestView : ModView
    {
        public override string Descr
        {
            get { return "Module for Scada REST-Service."; }
        }
    }
}
