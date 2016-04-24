using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Entity.Models;
using System.Runtime.Serialization;

namespace Entity
{
    [Class(NameType=typeof(Data))]
    public class Data : Idable
    {
        public Data() { }

        [Id(0, Type="int", Name="Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual double Value { get; set; }

        [Property]
        public virtual long Timestamp { get; set; }

        [ManyToOne(Name = "Device", Column = "Device", ClassType = typeof(Device), Cascade = "save-update", Lazy = Laziness.False)]
        public virtual Device Device { get; set; }

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
