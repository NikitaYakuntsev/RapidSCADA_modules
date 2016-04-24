using Entity.Models;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entity
{
    [Class(NameType = typeof(CommandLog))]
    public class CommandLog : Idable
    {
        public CommandLog() { }
        [Id(0, Type = "int", Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual long Timestamp { get; set; }

        [Property]
        public virtual bool Sent { get; set; }

        [ManyToOne(Name = "Command", Column = "Command", ClassType = typeof(Command), Cascade = "save-update", Lazy = Laziness.False)]
        public virtual Command Command { get; set; }

        [ManyToOne(Name = "Token", Column = "Token", ClassType = typeof(Token), Cascade = "save-update", Lazy = Laziness.False)]
        public virtual Token Token { get; set; }

        public virtual int GetId()
        {
            return this.Id;
        }
        public virtual void SetId(int id)
        {
            this.Id = id;
        }

        public virtual String toString()
        {
            return GetId().ToString();
        }
    }
}
