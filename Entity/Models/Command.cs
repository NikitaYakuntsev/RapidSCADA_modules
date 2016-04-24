using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Entity
{
    [Class(NameType=typeof(Command))]
    public class Command : Idable
    {
        [Id(0, Type="int", Name="Id")]
        [Generator(1, Class="native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual String Text { get; set; }

        [ManyToOne(Name = "Device", ClassType = typeof(Device), Cascade = "save-update")]
        public virtual Device Device { get; set; }


        [Bag(0, Name = "CommandRecords", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1)]
        [OneToMany(2, ClassType = typeof(CommandLog))]
        private IList<CommandLog> _commandRecords;
        public virtual IList<CommandLog> CommandRecords
        {
            get { return _commandRecords ?? (_commandRecords = new List<CommandLog>()); }
            set { _commandRecords = value; }
        }


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
