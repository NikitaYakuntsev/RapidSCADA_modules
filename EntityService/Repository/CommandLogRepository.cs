using Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class CommandLogRepository : CommonRepository<CommandLog>
    {
        private CommandLogRepository() { }

        private static CommandLogRepository instance = new CommandLogRepository();


        public static CommandLogRepository GetInstance()
        {
            if (instance == null)
                instance = new CommandLogRepository();
            return instance;
        }

        public CommandLog Update(int id, CommandLog objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        CommandLog old = (CommandLog)session.Get(objectToUpdate.GetType(), objectToUpdate.Id);
                        old.Name = objectToUpdate.Name;
                        old.Sent = objectToUpdate.Sent;
                        old.Timestamp = objectToUpdate.Timestamp;
                        old.Token = objectToUpdate.Token;
                        old.Command = objectToUpdate.Command;
                        session.Evict(old);
                        CommandLog newCL = session.Merge(old);
                        transaction.Commit();
                        return newCL;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public CommandLog GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM CommandLog WHERE Id = :id");
                query.SetParameter("id", objectId);
                return (CommandLog)query.UniqueResult();
            }
        }

        public override ICollection<CommandLog> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM CommandLog");
                ICollection<CommandLog> res = query.List<CommandLog>();
                return res;
            }
        }

        public ICollection<CommandLog> GetByCommand(int commandId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM CommandLog WHERE Command = :id");
                query.SetParameter("id", commandId);
                return query.List<CommandLog>();
            }
        }

        public ICollection<CommandLog> GetByDevice(int deviceId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("select cl FROM CommandLog cl INNER JOIN cl.Command com WHERE com.Device = :devid");
                query.SetParameter("devid", deviceId);
                return query.List<CommandLog>();
            }
        }

    }
}
