using Entity;
using EntityDTO.ModelsDTO;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Service
{
    public class DeviceService
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

        private DeviceService() { }
        private static DeviceService instance = new DeviceService();
        public static DeviceService GetInstance()
        {
            if (instance == null)
                instance = new DeviceService();
            return instance;
        }
    }
}
