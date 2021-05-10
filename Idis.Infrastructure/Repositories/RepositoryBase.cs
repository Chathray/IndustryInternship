using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Idis.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly DataContext _context;
        protected readonly IDataShaper<T> _dataShaper;


        public RepositoryBase(DataContext context)
        {
            _context = context;
            _dataShaper = new DataShaper<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                _context.Set<T>()
                .Where(expression)
                .AsNoTracking()
                :
                _context.Set<T>()
                .Where(expression);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ?
                _context.Set<T>()
                .AsNoTracking()
                :
                _context.Set<T>();
        }

        public T GetOne(int key)
        {
            return _context.Set<T>().Find(key);
        }

        public ExpandoObject GetOneShaped(int key, string fields)
        {
            var obj = GetOne(key);
            return _dataShaper.ShapeData(obj, fields);
        }

        public IList<T> GetAll(bool asNoTracking = true)
        {
            try
            {
                if (asNoTracking)
                    return FindAll(false).AsNoTracking().ToList();
                else
                    return FindAll(false).AsTracking().ToList();
            }
            catch (Exception ex)
            {
                Log.Error($"Func: {nameof(GetAll)}, " + ex.Message);
                return null;
            }
        }

        public IList<ExpandoObject> GetAllShaped(string fields)
        {
            var objs = GetAll();
            return _dataShaper.ShapeDatas(objs, fields);
        }

        public bool Update(T entity, string[] ignores = null)
        {
            _context.Set<T>().Update(entity);

            if (ignores != null)
            {
                foreach (var property in ignores)
                {
                    _context.Entry(entity).Property(property).IsModified = false;
                }
            }
            return SaveChanges(nameof(Update)) > 0;
        }

        public bool UpdateIncluded(T entity, string[] accepted = null)
        {
            try
            {
                _context.Set<T>().Update(entity);

                // Get all properties on the object
                var properties = entity.GetType().GetProperties()
                    .Where(x => x.CanRead)
                    .Where(x => x.GetValue(entity, null) != null)
                    .ToDictionary(x => x.Name, x => x.GetValue(entity, null));

                if (accepted != null)
                {
                    foreach (var property in properties)
                        foreach (var ok in accepted)
                        {
                            if (property.Key == ok)
                            {
                                _context.Entry(entity)
                                    .Property(property.Key).IsModified = true;
                            }
                            else
                            {
                                _context.Entry(entity)
                                    .Property(property.Key).IsModified = false;
                            }
                        }
                }
                var effected = SaveChanges(nameof(Update));
                return effected > 0;
            }
            catch (Exception ex)
            {
                Log.Error($"Func: {nameof(UpdateIncluded)}, {ex.Message}");
                return false;
            }
        }

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return SaveChanges(nameof(Create)) > 0;
        }

        public bool Delete(int key)
        {
            var obj = _context.Set<T>().Find(key);

            if (obj is null) return false;

            _context.Set<T>().Remove(obj);
            return SaveChanges(nameof(Delete)) > 0;
        }

        public int Count() => _context.Set<T>().Count();

        public int CountByIndex(int index)
        {
            return index switch
            {
                1 => _context.Departments.Count(),
                2 => _context.Events.Count(),
                3 => _context.EventTypes.Count(),
                4 => _context.Interns.Count(),
                5 => _context.Organizations.Count(),
                6 => _context.Points.Count(),
                7 => _context.Questions.Count(),
                8 => _context.Trainings.Count(),
                9 => _context.Users.Count(),
                _ => 0
            };
        }

        public int SaveChanges(string funcname)
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error($"Func: {funcname}, " + ex.Message);
                return 0;
            }
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
