using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Entity
{
    [Class]
    public class Device
    {
        [Id(0)]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual bool Working { get; set; }

        [Bag(0, Name="Data", Inverse = true)]
        [Key(1)]
        [OneToMany(2, ClassType = typeof(Data))]
        private IList<Data> _data;
        public virtual IList<Data> Data 
        { 
            get { return _data ?? (_data = new List<Data>()); } 
            set { _data = value; } 
        }
    }
}
