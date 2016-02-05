using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

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
        void GetOptions();
    }
}
