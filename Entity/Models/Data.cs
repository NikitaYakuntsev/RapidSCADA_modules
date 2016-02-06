using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Entity
{
    [Class]
    public class Data
    {
        [Id(0)]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual double Value { get; set; }

        [Property]
        public virtual long Timestamp { get; set; }

        [ManyToOne(Name = "Device", ClassType = typeof(Device), Cascade = "save-update")]
        public Device Device { get; set; }
    }
}
