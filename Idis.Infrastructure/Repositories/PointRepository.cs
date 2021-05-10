using Dapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Data;

namespace Idis.Infrastructure
{
    public class PointRepository : RepositoryBase<Point>, IPointRepository
    {
        public PointRepository(DataContext context) : base(context)
        { }

        public bool EvaluateIntern(Point point)
        {
            try
            {
                return _context.Database.GetDbConnection()
                              .Execute($@"CALL EvaluateIntern(
                           {point.InternId}
                         , {point.MarkerId}
                         , {point.TechnicalSkill}
                         , {point.SoftSkill}
                         , {point.Attitude})") > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Func: {nameof(EvaluateIntern)}");
                return false;
            }
        }

        public Point GetPointDetail(int id)
        {
            var point = GetOne(id);

            if (point is null) return null;

            _context.Entry(point).Reference(_ => _.Marker).Load();
            return point;
        }

        public IDataReader GetAllWithName()
        {
            return _context.Database.GetDbConnection()
                 .ExecuteReader($"CALL GetAllPointWithName()");
        }

        public int GetPassedCount()
        {
            return Convert.ToInt32(_context.Database.GetDbConnection()
                 .ExecuteScalar($"CALL HowManyPassed()"));
        }
    }
}
