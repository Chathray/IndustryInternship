using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Idis.Infrastructure
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        { }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            var boss = _context.Departments.Find(depId);

            if (boss is null) return false;

            var array = boss.SharedTrainings;

            if (array is not null && array.Length > 0)
            {
                if (!array.Contains("," + sharedId) && !array.Contains("" + sharedId))
                    array = array.Insert(array.Length, "," + sharedId);
            }
            else array = sharedId + "";

            var parameter = new DynamicParameters();
            parameter.Add("depId", depId);
            parameter.Add("sharedArray", array);

            return _context.Database.GetDbConnection()
                .Execute($"CALL SetSharedTraining(@depId,@sharedArray)", parameter) > 0;
        }
    }
}
