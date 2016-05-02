using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO.ModelsDTO
{
    public class DeviceDTO : Device
    {
        public List<CommandDTO> Commands { get; set; }
        public List<DataDTO> Datas { get; set; }

        public static DeviceDTO Convert(Device from)
        {
            DeviceDTO res = new DeviceDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Working = from.Working;

            res.Datas = new List<DataDTO>();
            foreach (Data d in from.Data)
                res.Datas.Add(DataDTO.Convert(d));

            res.Commands = new List<CommandDTO>();
            foreach (Command c in from.Command)
                res.Commands.Add(CommandDTO.Convert(c));

            return res;
        }
    }
}
