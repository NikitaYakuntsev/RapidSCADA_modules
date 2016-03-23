
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace EntityService.IRepository
{
    public interface IDeviceRepository
    {
        void Add(Device data);
        void Update(Device data);
        void Remove(Device data);
        Device GetById(int deviceId);
        ICollection<Device> GetAll();
    }
}
