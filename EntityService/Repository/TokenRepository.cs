using Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Repository
{
    public class TokenRepository : CommonRepository<Token>
    {
        private TokenRepository() { }

        private static TokenRepository instance = new TokenRepository();

        public static TokenRepository GetInstance()
        {
            if (instance == null)
                instance = new TokenRepository();
            return instance;
        }

        public Token Update(int id, Token objectToUpdate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Token old = (Token)session.Get(objectToUpdate.GetType(), objectToUpdate.Id);
                        old.CommandRecords = objectToUpdate.CommandRecords;
                        old.CreationDate = objectToUpdate.CreationDate;
                        old.ExpirationDate = objectToUpdate.ExpirationDate;
                        old.Name = objectToUpdate.Name;
                        Token newToken = session.Merge(old);
                        transaction.Commit();
                        return newToken;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Token GetById(int objectId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Token WHERE Id = :id");
                query.SetParameter("id", objectId);
                return (Token)query.UniqueResult();
            }
        }

        public override ICollection<Token> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("FROM Token");
                ICollection<Token> res = query.List<Token>();
                return res;
            }
        }
    }
}
