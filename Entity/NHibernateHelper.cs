using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;

namespace Entity
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var conf = new Configuration();
                    conf.Configure(@"~\Models\HNibernate\NHibernate.cfg.xml");
                    HbmSerializer.Default.Validate = true;

                    var stream = HbmSerializer.Default.Serialize(Assembly.GetAssembly(typeof(Device)));
                    conf.AddInputStream(stream);

                    stream = HbmSerializer.Default.Serialize(Assembly.GetAssembly(typeof(Data)));
                    conf.AddInputStream(stream);

                    ISessionFactory sessionFactory = conf.BuildSessionFactory();
                    new SchemaUpdate(conf).Execute(true, true);
                }
                return _sessionFactory;
            }
        }


        public static ISession OpenSession()
        {                    
            return SessionFactory.OpenSession();
        }
    }
}
