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

        public IList<T> GetAll()
        {
            return FindAll(false).AsNoTracking().ToList();
        }

        public IList<ExpandoObject> GetAllShaped(string fields)
        {
            var objs = GetAll();
            return _dataShaper.ShapeDatas(objs, fields);
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return SaveChanges(nameof(Update)) > 0;
            }
            catch (InfrastructureException ex)
            {
                Log.Information($"{ex.Message}");
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
            catch (InfrastructureException ex)
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
