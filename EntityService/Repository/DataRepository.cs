using Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class DataRepository : CommonRepository<Data>
    {
        private DataRepository() { }

        private static DataRepository instance = new DataRepository();


        public static DataRepository GetInstance()
        {
            if (instance == null)
                instance = new DataRepository();
            return instance;
        }


        public Data Update(int id, Data objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Data oldDev = (Data)session.Get(objectToUpdate.GetType(), objectToUpdate.GetId());
                        oldDev.Name = objectToUpdate.Name;                        
                        oldDev.Value = objectToUpdate.Value;
                        oldDev.Timestamp = objectToUpdate.Timestamp;
                        oldDev.Device = objectToUpdate.Device;
                        session.Evict(oldDev);
                        Data newDev = session.Merge(oldDev);
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


        public Data GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Data WHERE Id = :id");
                query.SetParameter("id", objectId);
                return (Data)query.UniqueResult();
            }
        }

        public override ICollection<Data> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Data");
                ICollection<Data> res = query.List<Data>();
                return res;
            }
        }

        public ICollection<Data> GetByDevice(int deviceId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Data WHERE Device = :id");
                query.SetParameter("id", deviceId);
                return query.List<Data>();                
            }
        }


    }
}
