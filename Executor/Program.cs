using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Executor
{
    public class Program
    {
        private static WebServiceHost host;
        private static String uriAddr = "http://0.0.0.0:8000/scada/";

        static void Main(string[] args)
        {
            //ModRestLogic logic = new ModRestLogic();
            //logic.OnServerStart();
            InitRest();
            StartRest();
            Console.ReadLine();
            StopRest();
        }

        /// <summary>
        /// Initializes REST.
        /// </summary>
        private static void InitRest()
        {
            host = new WebServiceHost(typeof(RestService), new Uri(uriAddr));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IRestService), new WebHttpBinding(), "");
            //WriteToLog("Rest initialized on " + uriAddr, Log.ActTypes.Information);
        }

        /// <summary>
        /// Starts host.
        /// </summary>
        private static void StartRest()
        {
            host.Open();
        }


        /// <summary>
        /// Stops host.
        /// </summary>
        private static void StopRest()
        {
            host.Close();
        }
    }
}
