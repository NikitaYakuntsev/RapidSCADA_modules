using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Entity;
using EntityDTO.ModelsDTO;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        IP getCurrentIp();

        [OperationContract]
        SystemMessage register(String key, String user);

        [OperationContract]
        List<DeviceDTO> getDevices();

        [OperationContract]
        Device getDevice(String deviceId);

        [OperationContract]
        List<CommandDTO> getDeviceCommands(String deviceId);

        [OperationContract]
        List<CommandLog> getDeviceCommandLog(String deviceId);

        [OperationContract]
        List<Data> getDeviceData(String deviceId);

        [OperationContract]
        List<DataDTO> getDeviceDataInPeriod(String deviceId, String from, String to);

        [OperationContract]
        System.Net.HttpStatusCode sendCommand(String deviceId, String commandId, String tokenId);

        [OperationContract]
        System.IO.Stream Connect(String filename);

        [OperationContract]
        System.Net.HttpStatusCode editDevice(Device device);

        [OperationContract]
        System.Net.HttpStatusCode editCommand(Command command);


        [OperationContract]
        void GetOptions();
    }
}
