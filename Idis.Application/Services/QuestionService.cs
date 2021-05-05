using Idis.Infrastructure;

namespace Idis.Application
{
    public class QuestionService : ServiceBase<QuestionModel, Question>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepo;
        public QuestionService(IQuestionRepository questionRepo) : base(questionRepo)
        {
            _questionRepo = questionRepo;
        }
    }
}
