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
using Entity.Models;

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
                    String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\NHibernate.cfg.xml";
                    conf.Configure(path);
                    HbmSerializer.Default.Validate = true;



                    //var stream = HbmSerializer.Default.Serialize(System.Reflection.Assembly.GetExecutingAssembly());
                    //stream.Position = 0;
                    var deviceStream = HbmSerializer.Default.Serialize(typeof(Device));
                    conf.AddInputStream(deviceStream);

                    var dataStream = HbmSerializer.Default.Serialize(typeof(Data));
                    conf.AddInputStream(dataStream);

                    var commStream = HbmSerializer.Default.Serialize(typeof(Command));
                    conf.AddInputStream(commStream);

                    var tokenStream = HbmSerializer.Default.Serialize(typeof(Token));
                    conf.AddInputStream(tokenStream);

                    var commLogStream = HbmSerializer.Default.Serialize(typeof(CommandLog));
                    conf.AddInputStream(commLogStream);

                    var syspropStream = HbmSerializer.Default.Serialize(typeof(SystemParameter));
                    conf.AddInputStream(syspropStream);                    

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
