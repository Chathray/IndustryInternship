using Dapper;
using Serilog;
using System;

namespace Idis.Infrastructure
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DataContext context) : base(context)
        { }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            var dep_object = _context.Departments.Find(depId);

            if (dep_object is null)
                return false;

            var newShared = dep_object.SharedTrainings;

            if (newShared is not null && newShared.Length > 0)
            {
                if (!newShared.Contains("," + sharedId)
                 && !newShared.Contains("" + sharedId))
                {
                    newShared = newShared.Insert(newShared.Length, sharedId + ",");
                }
            }
            else newShared = sharedId + ",";

            dep_object.SharedTrainings = newShared;

            var effected = SaveChanges(nameof(InsertSharedTraining));
            return effected > 0;
        }

        public bool RefreshSharedTrainings(int traId)
        {
            var list = GetAll(false).AsList();

            list.ForEach(dep =>
            {
                try
                {
                    dep.SharedTrainings = dep.SharedTrainings?.Replace($"{traId}", "");
                    dep.SharedTrainings = dep.SharedTrainings?.Replace($",,", ",");
                }
                catch (Exception ex)
                {
                    Log.Error($"Func: {nameof(RefreshSharedTrainings)}, {ex.Message}");
                }
            });

            var effected = SaveChanges(nameof(RefreshSharedTrainings));
            return effected > 0;
        }
    }
}
