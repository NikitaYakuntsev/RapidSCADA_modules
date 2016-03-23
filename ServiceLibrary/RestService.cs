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

        private DeviceRepository devRep = new DeviceRepository();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "system/ip")]
        public IP getCurrentIp()
        {
            AddCorsHeaders();

            Device device = new Device();
            device.Name = "TestDevice";
            device.Working = true;
            devRep.Save(device);

            Device andevice = new Device();
            andevice.Name = "TestDevice2";
            andevice.Working = false;
            devRep.Save(andevice);
            

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

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
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
            throw new NotImplementedException();
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
