using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO.ModelsDTO
{
    public class DataDTO : Data
    {
        public static DataDTO Convert(Data from)
        {
            DataDTO res = new DataDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Timestamp = from.Timestamp;
            res.Value = from.Value;
            return res;
        }
    }
}
