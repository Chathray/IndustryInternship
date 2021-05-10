using AutoMapper;

namespace Idis.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Application.UserModel, RegisterRequest>().ReverseMap();
            CreateMap<Application.EventModel, CreateEventRequest>().ReverseMap();
            CreateMap<Application.InternModel, CreateInternRequest>().ReverseMap();
            CreateMap<Application.QuestionModel, CreateQuestionRequest>().ReverseMap();
            CreateMap<Application.PointModel, EvaluateRequest>().ReverseMap();
        }
    }
}