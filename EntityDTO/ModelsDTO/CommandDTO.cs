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
        public CommandParameterDTO[] Params { get; set; } //not possible to do with lower case.
        public static CommandDTO Convert(Command from)
        {
            CommandDTO res = new CommandDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Text = from.Text;

            int size = new Random().Next(1,3);
            res.Params = new Random().Next() % 3 == 0 ? null : new CommandParameterDTO[size];
            if (res.Params != null)
            {
                for (int i = 0; i < size; i++)
                    res.Params[i] = new CommandParameterDTO();
            }
            return res;            
        }
    }
}
