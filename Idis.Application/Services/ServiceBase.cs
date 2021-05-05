using Idis.Infrastructure;
using System.Collections.Generic;

namespace Idis.Application
{
    public abstract class ServiceBase<M, E> : IServiceBase<M, E>
    {
        private readonly IRepositoryBase<E> _base;
        public ServiceBase(IRepositoryBase<E> inbase)
        {
            _base = inbase;
        }

        public M GetOne(int key)
        {
            E entity = _base.GetOne(key);
            return ObjectMapper.Mapper.Map<M>(entity);
        }

        public IList<M> GetAll()
        {
            var list = _base.GetAll();
            return ObjectMapper.Mapper.Map<IList<M>>(list);
        }

        public bool Update(M model)
        {
            var entity = ObjectMapper.Mapper.Map<E>(model);
            return _base.Update(entity);

        }

        public bool Create(M entity)
        {
            var model = ObjectMapper.Mapper.Map<E>(entity);
            return _base.Create(model);
        }

        public bool Delete(int key)
        {
            return _base.Delete(key);
        }

        public int Count()
        {
            return _base.Count();
        }

        public int CountByIndex(int index)
        {
            return _base.CountByIndex(index);
        }
    }
}
