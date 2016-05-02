using Entity;
using EntityDTO.ModelsDTO;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Facade
{
    public class DeviceFacade
    {
        private static DeviceRepository devRep = DeviceRepository.GetInstance();

        public List<DeviceDTO> GetAllDevices()
        {
            ICollection<Device> devices = devRep.GetAll();
            List<DeviceDTO> res = new List<DeviceDTO>();
            foreach (Device d in devices)
                res.Add(DeviceDTO.Convert(d));
            return res;
        }

        private DeviceFacade() { }
        private static DeviceFacade instance = new DeviceFacade();
        public static DeviceFacade GetInstance()
        {
            if (instance == null)
                instance = new DeviceFacade();
            return instance;
        }
    }
}
