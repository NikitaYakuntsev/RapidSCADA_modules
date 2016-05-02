using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO.ModelsDTO
{
    //Deprecated.
    public class CommandLogDTO : CommandLog
    {
        public CommandDTO CommandDTO { get; set; }
        public TokenDTO TokenDTO { get; set; }
        
        public static CommandLogDTO Convert(CommandLog from)
        {
            CommandLogDTO res = new CommandLogDTO();
            res.Id = from.Id;
            res.Name = from.Name;
            res.Sent = from.Sent;
            res.Timestamp = from.Timestamp;
            res.TokenDTO = TokenDTO.Convert(from.Token);
            res.CommandDTO = CommandDTO.Convert(from.Command);
            return res;
        }
    }
}
