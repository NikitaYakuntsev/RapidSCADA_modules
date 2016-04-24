using Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class DeviceRepository : CommonRepository<Device>
    {
        private DeviceRepository()
        {
           
        }

        private static DeviceRepository instance = new DeviceRepository();


        public static DeviceRepository GetInstance()
        {
            if (instance == null)
                instance = new DeviceRepository();
            return instance;
        }

        public Device Update(int id, Device objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Device oldDev = (Device)session.Get(objectToUpdate.GetType(), objectToUpdate.GetId());
                        oldDev.Name = objectToUpdate.Name;
                        oldDev.Working = objectToUpdate.Working;
                        oldDev.Data = objectToUpdate.Data;
                        oldDev.Command = objectToUpdate.Command;
                        session.Update(oldDev);
                        transaction.Commit();
                        return null;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }


        public Device GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Device WHERE Id = :id");
                query.SetParameter("id", objectId);
                Device res = (Device)query.UniqueResult();
                return res;
            }
        }

        public override ICollection<Device> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Device");
                ICollection<Device> res = query.List<Device>(); 
                return res;
            }
        }
    }
}
