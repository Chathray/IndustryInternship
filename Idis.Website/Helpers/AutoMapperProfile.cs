using AutoMapper;

namespace Idis.Website
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Normal
            CreateMap<Application.InternModel, IndexViewModel>().ReverseMap();
            CreateMap<Application.EventModel, CalendarViewModel>().ReverseMap();
            CreateMap<Application.PointModel, PointViewModel>().ReverseMap();
            CreateMap<Application.QuestionModel, QuestionViewModel>().ReverseMap();

            CreateMap<UserViewModel, Application.UserModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.RegiterPassword))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegiterEmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RegiterFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RegiterLastName))
                .ReverseMap();

            CreateMap<SettingsViewModel, Application.UserModel>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department))
                .ReverseMap();
        }
    }
}