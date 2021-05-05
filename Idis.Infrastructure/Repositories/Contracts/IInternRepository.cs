using System.Collections.Generic;
using System.Data;

namespace Idis.Infrastructure
{
    public interface IInternRepository : IRepositoryBase<Intern>
    {
        dynamic GetInternInfo(int id);
        DataSet GetInternModelList(int page, int size, int sort, int search_on, string search_string);
        DataSet GetInternModelList(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date);

        IList<Intern> GetInternByPage(int page, int size);
        dynamic GetInternDetail(int id);
        IList<Training> GetJointTrainings(int internId);
        dynamic GetWhitelist();
        Training GetTraining(int internId);
    }
}
