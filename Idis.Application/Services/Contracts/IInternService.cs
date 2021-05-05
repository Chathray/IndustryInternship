using Idis.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace Idis.Application
{
    public interface IInternService : IServiceBase<InternModel, Intern>
    {
        dynamic GetInternInfo(int id);
        dynamic GetInternDetail(int id);
        IList<InternModel> GetInternByPage(int page, int size);
        IList<InternListModel> GetInternByPage(int page, int size, string sort);
        DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string);
        DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date);
        IList<TrainingModel> GetJointTrainings(int internId);
        dynamic GetWhitelist();
        TrainingModel GetTraining(int internId);
    }
}