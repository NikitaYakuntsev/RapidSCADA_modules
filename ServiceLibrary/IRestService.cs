using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        IP getCurrentIp();

        [OperationContract]
        SystemMessage register(String publicKey);

        [OperationContract]
        List<Device> getDevices();

        [OperationContract]
        Device getDevice(String deviceId);

        [OperationContract]
        List<Command> getDeviceCommands(String deviceId);

        [OperationContract]
        List<Data> getDeviceData(String deviceId);

        [OperationContract]
        String sendCommand(String deviceId, String commandText);


        [OperationContract]
        void GetOptions();
    }
}
