using Idis.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace Idis.Application
{
    public class InternService : ServiceBase<InternModel, Intern>, IInternService
    {
        private readonly IInternRepository _internRepo;
        public InternService(IInternRepository internRepo) : base(internRepo)
        {
            _internRepo = internRepo;
        }


        public DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string)
        {
            return _internRepo.GetInternModelList(currentPage, pageSize, sort, search_on, search_string);
        }

        public DataSet GetInternByPage(int currentPage, int pageSize, int sort, int search_on, string search_string, int on_passed, int date_filter, string start_date, string end_date)
        {
            return _internRepo.GetInternModelList(currentPage, pageSize, sort, search_on, search_string, on_passed, date_filter, start_date, end_date);
        }

        public IList<InternListModel> GetInternByPage(int page, int size, string sort)
        {
            var list = _internRepo.GetInternByPage(page, size);

            return ObjectMapper.Mapper.Map<IList<InternListModel>>(list);
        }

        public IList<InternModel> GetInternByPage(int page, int size)
        {
            var obj = _internRepo.GetInternByPage(page, size);
            return ObjectMapper.Mapper.Map<IList<InternModel>>(obj);
        }

        public dynamic GetInternInfo(int id)
        {
            return _internRepo.GetInternInfo(id);
        }

        public dynamic GetInternDetail(int id)
        {
            return _internRepo.GetInternDetail(id);
        }


        public IList<TrainingModel> GetJointTrainings(int internId)
        {
            IList<Training> obj = _internRepo.GetJointTrainings(internId);

            return ObjectMapper.Mapper.Map<IList<Training>, IList<TrainingModel>>(obj);
        }

        public dynamic GetWhitelist()
        {
            return _internRepo.GetWhitelist();
        }

        public TrainingModel GetTraining(int internId)
        {
            var entity = _internRepo.GetTraining(internId);
            return ObjectMapper.Mapper.Map<Training, TrainingModel>(entity);
        }
    }
}
