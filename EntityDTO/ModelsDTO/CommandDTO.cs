using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO.ModelsDTO
{
    public class CommandDTO : Command
    {
        public String expand = "";
        public String direction = "down";
        public static CommandDTO Convert(Command from)
        {
            CommandDTO res = new CommandDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Text = from.Text;
            return res;            
        }
    }
}
