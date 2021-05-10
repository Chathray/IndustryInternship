using AutoMapper;

namespace Idis.Website
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Normal
            CreateMap<IndexViewModel, Application.InternModel>();
            CreateMap<CalendarViewModel, Application.EventModel>();
            CreateMap<PointViewModel, Application.PointModel>();
            CreateMap<QuestionViewModel, Application.QuestionModel>();

            CreateMap<UserViewModel, Application.UserModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.RegiterPassword))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RegiterEmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RegiterFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RegiterLastName));

            // Reverse
            CreateMap<SettingsViewModel, Application.UserModel>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department))
                .ReverseMap();
        }
    }
}