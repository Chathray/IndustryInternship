using AutoMapper;

namespace Idis.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, Application.UserModel>();
            CreateMap<CreateEventRequest, Application.EventModel>();
            CreateMap<CreateInternRequest, Application.InternModel>();
            CreateMap<CreateQuestionRequest, Application.QuestionModel>();
        }
    }
}