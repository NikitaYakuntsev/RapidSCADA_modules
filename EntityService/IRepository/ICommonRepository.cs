using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;


namespace EntityService.IRepository
{
    public interface ICommonRepository<T>
    {
        void Add(T objectToAdd);
        void Update(T objectToUpdate);
        void Remove(T objectToRemove);
        T GetById(int objectId);
        ICollection<T> GetAll();
    }
}
