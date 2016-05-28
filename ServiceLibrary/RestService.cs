using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Entity;
using EntityService.Repository;
using EntityService.Service;
using Entity.Models;
using EntityDTO.ModelsDTO;
using System.IO;
using System.Reflection;

namespace ServiceLibrary
{
    public class RestService : IRestService
    {

        private static DeviceRepository devRep = DeviceRepository.GetInstance();
        private static DataRepository dataRep = DataRepository.GetInstance();
        private static CommandRepository commRep = CommandRepository.GetInstance();
        private static CommandLogRepository commLogRep = CommandLogRepository.GetInstance();
        private static TokenRepository tokRep = TokenRepository.GetInstance();
        private static SystemParameterRepository spRep = SystemParameterRepository.GetInstance();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "system/ip")]
        public IP getCurrentIp()
        {
            AddCorsHeaders();         

            String strip = Ipify.GetPublicAddress();
            return new IP()
            {
                ip = strip
            };
        }

        [WebInvoke(Method = "POST", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "register")]
        public SystemMessage register(String key, String user)
        {
            AddCorsHeaders();
            String id = spRep.GetByKey(Common.Dictionary.SystemProperties.SP_SCADA_ID).Value;
            return new SystemMessage()
            {
                id = id,
                name = "RapidSCADA",
                key = key
            };
        }

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "device/all")]
        public List<DeviceDTO> getDevices()
        {
            AddCorsHeaders();
            //var res = devRep.GetAll().ToList();            
            //return res;
            return DeviceService.GetInstance().GetAllDevices();
        }

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}")]
        public Device getDevice(String deviceId)
        {
            AddCorsHeaders();
            return devRep.GetById(Int32.Parse(deviceId));
        }

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/command/all")]
        public List<CommandDTO> getDeviceCommands(String deviceId)
        {
            AddCorsHeaders();
            List<CommandDTO> res = CommandService.GetInstance().GetByDevice(Int32.Parse(deviceId)).ToList();
            
            //List<Command> res = commRep.GetByDevice(Int32.Parse(deviceId)).ToList();
            return res;
        }

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/command/log")]
        public List<CommandLog> getDeviceCommandLog(String deviceId)
        {
            AddCorsHeaders();
            List<CommandLog> res = commLogRep.GetByDevice(Int32.Parse(deviceId)).ToList();
            return res;
        }

        [WebInvoke(Method = "GET", 
            BodyStyle=WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/data/all")]
        public List<Data> getDeviceData(String deviceId)
        {
            AddCorsHeaders();
            List<Data> res = dataRep.GetByDevice(Int32.Parse(deviceId)).ToList();
            return res;
        }

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/data/?from={from}&to={to}")]
        public List<DataDTO> getDeviceDataInPeriod(string deviceId, string from, string to)
        {
            AddCorsHeaders();
            return DataService.GetInstance().GetDatasByDevice(Int32.Parse(deviceId), from, to);
        }

        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json, 
            UriTemplate = "device/{deviceId}/command")]
        public System.Net.HttpStatusCode sendCommand(String deviceId, String commandId, String tokenId)
        {
            AddCorsHeaders();
            try {
                CommandService.GetInstance().AddCommandToQueue(Int32.Parse(commandId), Int32.Parse(tokenId));
                return System.Net.HttpStatusCode.Created;
            } catch (Exception e) {
                throw e;
            }
           
        }

        
        [WebInvoke(Method = "OPTIONS", 
            UriTemplate = "*")]
        public void GetOptions()
        {
            AddCorsHeaders();
        }
        

        private void AddCorsHeaders()
        {
            var request = WebOperationContext.Current.IncomingRequest;
            var response = WebOperationContext.Current.OutgoingResponse;

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-Requested-With");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
        }






        [System.ServiceModel.Web.WebGet( UriTemplate = "html/{*filename}" , BodyStyle = WebMessageBodyStyle.Bare )]
        public Stream Connect(String filename)
        {
            List<String> dirs = filename.Split('/').ToList();
            string file = dirs.ElementAt(dirs.Count - 1);
            dirs.RemoveAt(dirs.Count - 1);
            StringBuilder assPath = new StringBuilder(50);
            foreach (string dir in dirs)
                assPath.Append(dir + ".");
            assPath.Append(file);
            var slName = Assembly.GetExecutingAssembly().GetName().Name;
            String fullPath = String.Format("{0}.{1}", slName, assPath.ToString());
       
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath);            
        }


        //Endpoints for Rapid SCADA
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "device")]
        public System.Net.HttpStatusCode editDevice(Device device)
        {
            //dummy device validation
            //dummy update
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "command")]
        public System.Net.HttpStatusCode editCommand(Command command)
        {
            //dummy command validation
            //dummy update
            throw new NotImplementedException();
        }
    }
}
