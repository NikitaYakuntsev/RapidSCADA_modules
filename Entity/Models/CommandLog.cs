using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class CommandLog : Idable
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }

        public virtual int GetId()
        {
            return this.Id;
        }
    }
}
