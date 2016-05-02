using Entity;
using Entity.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class SystemParameterRepository : CommonRepository<SystemParameter>
    {

        public void Register(string scadaId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {     
                        SystemParameter registered = GetByKey(Common.Dictionary.SystemProperties.SP_REGISTERED);
                        registered.Value = true.ToString();
                        session.Evict(registered);
                        session.Merge(registered);

                        SystemParameter scadaID = GetByKey(Common.Dictionary.SystemProperties.SP_SCADA_ID);
                        scadaID.Value = scadaId;
                        session.Evict(scadaID);
                        session.Merge(scadaID);

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public SystemParameter Update(int id, SystemParameter objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        SystemParameter old = (SystemParameter)session.Get(objectToUpdate.GetType(), objectToUpdate.Id);
                        old.Value = objectToUpdate.Value;
                        session.Evict(old);
                        SystemParameter newSp = session.Merge(old);
                        transaction.Commit();
                        return newSp;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public SystemParameter GetByKey(string key)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM SystemParameter WHERE Key = :key");
                query.SetParameter("key", key);
                return (SystemParameter)query.UniqueResult();
            }
        }

        public SystemParameter GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM SystemParameter WHERE Id = :id");
                query.SetParameter("id", objectId);
                return (SystemParameter)query.UniqueResult();
            }
        }

        public override ICollection<SystemParameter> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM SystemParameter");
                ICollection<SystemParameter> res = query.List<SystemParameter>();
                return res;
            }
        }

        private SystemParameterRepository() { }
        private static SystemParameterRepository instance = new SystemParameterRepository();

        public static SystemParameterRepository GetInstance()
        {
            if (instance == null)
                instance = new SystemParameterRepository();
            return instance;
        }
    }
}
