using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Idis.Infrastructure
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

        T GetOne(int id);
        ExpandoObject GetOneShaped(int key, string fields);
        IList<T> GetAll();
        IList<ExpandoObject> GetAllShaped(string fields);

        bool Update(T obj);
        bool Create(T obj);
        bool Delete(int id);
        int Count();
        int CountByIndex(int index);

        Task<IList<T>> GetAllAsync();
        Task<int> GetCountAsync();
        int SaveChanges(string funcname);
    }
}
