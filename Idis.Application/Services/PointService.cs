using Idis.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace Idis.Application
{
    public class PointService : ServiceBase<PointModel, Point>, IPointService
    {
        private readonly IPointRepository _pointRepo;
        public PointService(IPointRepository pointRepo) : base(pointRepo)
        {
            _pointRepo = pointRepo;
        }


        public bool EvaluateIntern(PointModel mark)
        {
            var point = ObjectMapper.Mapper.Map<Point>(mark);
            return _pointRepo.EvaluateIntern(point);
        }


        public IList<PointListModel> GetAllWithName()
        {
            DataTable table = new();
            table.Load(_pointRepo.GetAllWithName());

            return AppExtensions.ConvertDataTable<PointListModel>(table);
        }


        public PointListModel GetPointDetail(int id)
        {
            var point = _pointRepo.GetPointDetail(id);
            if (point is null) return null;

            var obj = ObjectMapper.Mapper.Map<PointListModel>(point);
            obj.Marker = $"{point.Marker.FirstName} {point.Marker.LastName}";
            return obj;
        }


        public int GetPassedCount()
        {
            return _pointRepo.GetPassedCount();
        }
    }
}
