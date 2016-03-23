using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;
using System.IO;

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
                    conf.Configure(@"D:\NHibernate.cfg.xml");
                    HbmSerializer.Default.Validate = true;



                    //var stream = HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly());
                    //stream.Position = 0;
                    var stream = HbmSerializer.Default.Serialize(typeof(Device));
                    conf.AddInputStream(stream);


                    //_sessionFactory = conf.BuildSessionFactory();
                    var stream2 = HbmSerializer.Default.Serialize(typeof(Data));
                    conf.AddInputStream(stream2);
                    _sessionFactory = conf.BuildSessionFactory();
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
