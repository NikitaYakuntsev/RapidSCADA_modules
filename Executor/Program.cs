using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;
using SchedulerJob;

namespace Executor
{
    public class Program
    {
        private static WebServiceHost host;
        private static IScheduler scheduler;
        private static String uriAddr = "http://0.0.0.0:8000/scada/";
        private static int PERIOD = 60; //probably get it from system table.

        static void Main(string[] args)
        {
            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };            
            InitRest();
            InitScheduler();
            StartRest();
            Console.ReadLine();
            StopRest();
            scheduler.Shutdown();
        }

        private static void InitScheduler()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<HeartbeatJob>().WithIdentity("job1", "group1").Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1").StartNow().WithSimpleSchedule(
                x => x.WithIntervalInSeconds(PERIOD).RepeatForever()).Build();
            scheduler.ScheduleJob(job, trigger);
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
