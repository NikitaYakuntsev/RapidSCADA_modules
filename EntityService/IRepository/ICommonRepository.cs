using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;


namespace EntityService.IRepository
{
    public interface ICommonRepository<T>
        where T : Idable
    {
        void Save(T objectToAdd);
        T Update(int id, T objectToUpdate);
        void Remove(int id);
        T GetById(int objectId);
        ICollection<T> GetAll();
    }
}
