using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityDTO.ModelsDTO
{
    public class CommandParameterDTO
    {
        private static Random rnd = new Random();
        public CommandParameterDTO()
        {
            String[] arr = new String[] { "Switch on", "Switch after", "Switch at" };
            String[] types = new String[] { "date", "time", "datetime", "number", "string" };
            //add custom initialization logic here.
            required = rnd.Next() % 2 == 0 ? "required" : "";
            placeholder = arr[rnd.Next(arr.Count())];
            type = types[rnd.Next(types.Count())];
            validate = "*";
        }

        public String required { get; set; }
        public String placeholder { get; set; }
        public String type { get; set; }
        public String validate { get; set; }
            
    }
}
