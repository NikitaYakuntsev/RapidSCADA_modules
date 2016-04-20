using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;
using Entity.Models;

namespace Entity
{
    [Class(NameType=typeof(Device))]
    public class Device : Idable
    {
        public Device()
        {
        }

        [Id(0, Type="integer", Name="Id")]
        [Generator(1, Class = "native")]
        public virtual int Id { get; set; }

        [Property]
        public virtual String Name { get; set; }

        [Property]
        public virtual bool Working { get; set; }

        
        [Bag(0, Name="Data", Inverse = true, Lazy = CollectionLazy.False)]
        [Key(1)]
        [OneToMany(2, ClassType = typeof(Data))]
        private IList<Data> _data;
        public virtual IList<Data> Data 
        { 
            get { return _data ?? (_data = new List<Data>()); } 
            set { _data = value; } 
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
