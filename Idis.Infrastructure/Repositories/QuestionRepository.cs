namespace Idis.Infrastructure
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(DataContext context) : base(context)
        { }
    }
}
