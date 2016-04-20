using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Entity;
using EntityService.Repository;

namespace ServiceLibrary
{
    public class RestService : IRestService
    {

        private DeviceRepository devRep = DeviceRepository.GetInstance();
        private DataRepository dataRep = DataRepository.GetInstance();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "system/ip")]
        public IP getCurrentIp()
        {
            AddCorsHeaders();

            /*
            Device device = new Device();
            device.Name = "TestDevice" + new Random().Next();
            device.Working = true;
            device.Id = devRep.Save(device);

            Data data = new Data();
            data.Device = device;
            data.Name = "temp";
            data.Timestamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).Seconds;
            data.Value = new Random().Next();
            data.Id = dataRep.Save(data);
            */
            //device.Data.Add(data);
            //devRep.Update(device.Id, device);
            
            

            String strip = Ipify.GetPublicAddress();
            return new IP()
            {
                ip = strip
            };



        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "register")]
        public SystemMessage register(String publicKey)
        {
            AddCorsHeaders();
            
            return new SystemMessage()
            {
                id = "12345",
                name = "RapidSCADA",
                key = publicKey
            };
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "device/all")]
        public List<Device> getDevices()
        {
            AddCorsHeaders();

            var res = devRep.GetAll().ToList();
            return res;
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "device/{deviceId}")]
        public Device getDevice(String deviceId)
        {
            AddCorsHeaders();

            return devRep.GetById(Int32.Parse(deviceId));
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/command/all")]
        public List<Command> getDeviceCommands(String deviceId)
        {
            AddCorsHeaders();
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "device/{deviceId}/data/all")]
        public List<Data> getDeviceData(String deviceId)
        {
            AddCorsHeaders();
            return /*new List<Data>(dataRep.GetByDevice(Int32.Parse(deviceId)));   //*/
                new List<Data>(devRep.GetById(Int32.Parse(deviceId)).Data);
        }


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "device/{deviceId}/command")]
        public string sendCommand(String deviceId, string commandText)
        {
            AddCorsHeaders();
            throw new NotImplementedException();
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


    }
}
