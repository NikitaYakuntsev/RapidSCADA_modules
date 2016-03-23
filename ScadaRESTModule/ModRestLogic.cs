using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scada.Data;
using Utils;
using ServiceLibrary;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace Scada.Server.Modules
{
    public class ModRestLogic : ModLogic
    {
        private WebServiceHost host;
        private String uriAddr = "http://0.0.0.0:8000/scada/";

        public override string Name
        {
            get { return "Scada REST Module"; }         
        }

        public override void OnServerStart()
        {
            WriteToLog("Module ModRestLogic loaded.", Log.ActTypes.Information);

            base.OnServerStart();
            InitRest();
            StartRest();            
        }

        public override void OnServerStop()
        {
            StopRest();
            base.OnServerStop();
        }

        /// <summary>
        /// Initializes REST.
        /// </summary>
        private void InitRest()
        {            
            host = new WebServiceHost(typeof(RestService), new Uri(uriAddr));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IRestService), new WebHttpBinding(), "");
            WriteToLog("Rest initialized on " + uriAddr, Log.ActTypes.Information);
        }

        /// <summary>
        /// Starts host.
        /// </summary>
        private void StartRest()
        {
            host.Open();
        }


        /// <summary>
        /// Stops host.
        /// </summary>
        private void StopRest()
        {
            host.Close();
        }

    }
}
