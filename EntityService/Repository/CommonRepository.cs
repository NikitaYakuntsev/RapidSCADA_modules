using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using EntityService.IRepository;

namespace EntityService.Repository
{
    public class CommonRepository<T> : ICommonRepository<T>
    {

        public void Add(T objectToAdd)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(objectToAdd);
                    transaction.Commit();
                }
            }
        }

        public void Update(T objectToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Remove(T objectToRemove)
        {
            throw new NotImplementedException();
        }

        public T GetById(int objectId)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
