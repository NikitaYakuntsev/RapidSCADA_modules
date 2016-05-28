using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityDTO.ModelsDTO
{
    public class CommandParameterDTO
    {
        public CommandParameterDTO()
        {
            //add custom initialization logic here.
        }

        public String required { get; set; }
        public String placeholder { get; set; }
        public String type { get; set; }
        public String validate { get; set; }
            
    }
}
