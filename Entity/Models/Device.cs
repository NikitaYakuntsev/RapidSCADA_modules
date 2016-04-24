using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Entity.Models;
using System.Runtime.Serialization;

namespace Entity
{
    [Class(NameType=typeof(Device))]
    public class Device : Idable
    {
        public Device() { }

        [Id(0, Type="integer", Name="Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual bool Working { get; set; }

        [IgnoreDataMember]
        [Bag(0, Name="Data", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1, Column = "Device")]
        [OneToMany(2, ClassType = typeof(Data))]
        private IList<Data> _data;
        [IgnoreDataMember]
        public virtual IList<Data> Data 
        { 
            get { return _data ?? (_data = new List<Data>()); } 
            set { _data = value; } 
        }

        [IgnoreDataMember]
        [Bag(0, Name = "Command", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1, Column = "Device")]
        [OneToMany(2, ClassType = typeof(Command))]
        private IList<Command> _command;
        [IgnoreDataMember]
        public virtual IList<Command> Command
        {
            get { return _command ?? (_command = new List<Command>()); }
            set { _command = value; }
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
