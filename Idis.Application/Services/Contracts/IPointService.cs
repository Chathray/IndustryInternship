using System.Collections.Generic;
using System.Drawing;

namespace Idis.Application
{
    public interface IPointService : IServiceBase<PointModel, Point>
    {
        bool EvaluateIntern(PointModel mark);
        IList<PointListModel> GetAllWithName();
        int GetPassedCount();
        PointListModel GetPointDetail(int id);
    }
}