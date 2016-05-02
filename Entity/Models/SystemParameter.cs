using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Models
{
    [Class(NameType=typeof(SystemParameter))]
    public class SystemParameter : Idable
    {
        public SystemParameter() { }

        [Id(0, Type = "integer", Name = "Id")]
        [Generator(1, Class = "native")]     
        public virtual int Id { get; set; }

        [Property(Unique=true, Index = "IDX_SP_Key")]
        public virtual String Key { get; set; }

        [Property]
        public virtual String Value { get; set; }

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
