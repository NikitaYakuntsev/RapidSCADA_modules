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
        public DataTypeDTO Type { get; set; }
        public static DataDTO Convert(Data from)
        {
            DataTypeDTO t = new DataTypeDTO();
            t.Id = 1;
            t.Name = "Temperature";
            DataDTO res = new DataDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Timestamp = from.Timestamp;
            res.Value = from.Value;
            res.Type = t;
            return res;
        }
    }
}
