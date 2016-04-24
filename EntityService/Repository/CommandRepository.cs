using Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class CommandRepository : CommonRepository<Command>
    {
        private CommandRepository() { }

        private static CommandRepository instance = new CommandRepository();

        public static CommandRepository GetInstance()
        {
            if (instance == null)
                instance = new CommandRepository();
            return instance;
        }

        public Command Update(int id, Command objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Command oldCom = (Command)session.Get(objectToUpdate.GetType(), objectToUpdate.GetId());
                        oldCom.Name = objectToUpdate.Name;
                        oldCom.Text = objectToUpdate.Text;
                        oldCom.Device = objectToUpdate.Device;
                        Command newCom = session.Merge(oldCom);
                        transaction.Commit();
                        return newCom;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }

        }

        public Command GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Command WHERE Id = :id");
                query.SetParameter("id", objectId);
                return (Command)query.UniqueResult();
            }
        }

        public override ICollection<Command> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Command");
                ICollection<Command> res = query.List<Command>();
                return res;
            }
        }

        public ICollection<Command> GetByDevice(int deviceId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Command WHERE Device = :id");
                query.SetParameter("id", deviceId);
                return query.List<Command>();
            }
        }
    }
}
