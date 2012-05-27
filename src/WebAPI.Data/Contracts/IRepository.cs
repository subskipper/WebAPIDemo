using System.Collections.Generic;

namespace WebAPI.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void DeleteById(int id);
    }
}
