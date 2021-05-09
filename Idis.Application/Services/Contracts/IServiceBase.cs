using System.Collections.Generic;

namespace Idis.Application
{
    public interface IServiceBase<M, E>
    {
        M GetOne(int id);
        IList<M> GetAll();

        bool Update(M obj, string[] ignores = null);
        bool UpdateIncluded(M model, string[] accepted);

        bool Create(M obj);
        bool Delete(int id);
    }
}
