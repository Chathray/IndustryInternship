using System.Collections.Generic;

namespace Idis.Application
{
    public interface IServiceBase<M, E>
    {
        M GetOne(int id);
        IList<M> GetAll();
        bool Update(M obj);
        bool Create(M obj);
        bool Delete(int id);
    }
}
