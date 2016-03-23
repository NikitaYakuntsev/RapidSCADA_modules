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
        /*
        T Update(int id, T objectToUpdate);
        T GetById(int objectId);
        ICollection<T> GetAll(); 
        */

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
                        Device newDev = session.Merge(oldDev);
                        transaction.Commit();
                        return newDev;
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
                return (Device)query.UniqueResult();
            }
        }

        public override ICollection<Device> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Device");
                return query.List<Device>();
            }
        }
    }
}
