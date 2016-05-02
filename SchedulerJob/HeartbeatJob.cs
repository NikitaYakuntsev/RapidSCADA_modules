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

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Scheduled Job: " + DateTime.Now);

        }
    }
}
