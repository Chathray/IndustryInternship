using Idis.Infrastructure;

namespace Idis.Application
{
    public class DepartmentService : ServiceBase<DepartmentModel, Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepo;
        public DepartmentService(IDepartmentRepository departmentRepo) : base(departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public bool InsertSharedTraining(int sharedId, int depId)
        {
            return _departmentRepo.InsertSharedTraining(sharedId, depId);
        }
    }
}
