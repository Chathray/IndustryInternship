namespace Idis.WebApi
{
    public class CreateQuestionRequest
    {
        public int? QuestionId { get; set; }
        public string Group { get; set; }
        public string InData { get; set; }
        public string OutData { get; set; }
    }
}