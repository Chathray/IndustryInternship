using System.Data;

namespace Idis.Infrastructure
{
    public interface IPointRepository : IRepositoryBase<Point>
    {
        bool EvaluateIntern(Point point);
        IDataReader GetAllWithName();
        int GetPassedCount();
        Point GetPointDetail(int id);
    }
}
