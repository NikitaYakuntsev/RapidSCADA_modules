using EntityService.Repository;
using Entity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerJob
{
    public class HeartbeatJob : IJob
    {

        private static SystemParameterRepository sysRep = SystemParameterRepository.GetInstance();

        public void Execute(IJobExecutionContext context)
        {
            //Console.WriteLine("Scheduled Job: " + DateTime.Now);
            string url = sysRep.GetByKey(Common.Dictionary.SystemProperties.SP_CLOUD_HOST).Value;
            string ep = Common.Dictionary.SystemProperties.EP_HEARTBEAT;
            string id = sysRep.GetByKey(Common.Dictionary.SystemProperties.SP_SCADA_ID).Value;
            string host = String.Format("{0}/{1}/{2}", url, ep, id);
            String answer = Common.HttpRequest.WebRequest(host);
        }

        public void AddData(int deviceId = 5)
        {
            /*
            Device d = devRep.GetById(deviceId);

            Data dt = new Data();
            dt.Device = d;
            dt.Name = "Temperature";
            dt.Timestamp = Common.Utils.GetUnixTime();
            dt.Value = new Random().Next(-15, 50);
            dataRep.Save(dt);
             * */
        }

    }
}
