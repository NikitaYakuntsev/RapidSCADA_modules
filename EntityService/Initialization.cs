using Entity.Models;
using EntityService.Service;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService
{
    public class Initialization
    {
        private static SystemParameterRepository spr = SystemParameterRepository.GetInstance();
        public static void Initialize()
        {
            if (SystemPropertyService.GetInstance().GetValue(Common.Dictionary.SystemProperties.SP_SCADA_NAME) == null)
            {
                SystemParameter scadaName = new SystemParameter();
                scadaName.Key = Common.Dictionary.SystemProperties.SP_SCADA_NAME;
                scadaName.Value = "RapidScada";
                spr.Save(scadaName);
            }

            if (SystemPropertyService.GetInstance().GetValue(Common.Dictionary.SystemProperties.SP_CLOUD_HOST) == null)
            {
                SystemParameter cloudIp = new SystemParameter();
                cloudIp.Key = Common.Dictionary.SystemProperties.SP_CLOUD_HOST;
                cloudIp.Value = "http://scadadiploma.azurewebsites.net";
                spr.Save(cloudIp);
            }

            if (SystemPropertyService.GetInstance().GetValue(Common.Dictionary.SystemProperties.SP_SCADA_ID) == null)
            {
                SystemParameter scadaId = new SystemParameter();
                scadaId.Key = Common.Dictionary.SystemProperties.SP_SCADA_ID;
                scadaId.Value = "";
                spr.Save(scadaId);
            }

            if (SystemPropertyService.GetInstance().GetValue(Common.Dictionary.SystemProperties.SP_REGISTERED) == null)
            {
                SystemParameter registered = new SystemParameter();
                registered.Key = Common.Dictionary.SystemProperties.SP_REGISTERED;
                registered.Value = false.ToString();
                spr.Save(registered);
            }


        }

        public static void Register()
        {
            if (!SystemPropertyService.GetInstance().IsRegistered())
            {
                String url = spr.GetByKey(Common.Dictionary.SystemProperties.SP_CLOUD_HOST).Value;
                String regEp = Common.Dictionary.SystemProperties.EP_REGISTER;
                String host = String.Format("{0}/{1}", url, regEp);
                string scadaId = Common.Utils.GetJsonToken(Common.HttpRequest.WebRequest(host), "Uid");
                spr.Register(scadaId);
            }
        }
    }
}
