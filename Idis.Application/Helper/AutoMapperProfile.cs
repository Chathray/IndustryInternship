using AutoMapper;
using System;

namespace Idis.Application
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var configuration = new MapperConfiguration(config =>
            {
                // This line ensures that internal properties are also mapped over.
                config.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                config.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = configuration.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;

        public class AspnetRunDtoMapper : Profile
        {
            public AspnetRunDtoMapper()
            {
                CreateMap<Infrastructure.User, UserModel>().ReverseMap();
                CreateMap<Infrastructure.Event, EventModel>().ReverseMap();
                CreateMap<Infrastructure.Question, QuestionModel>().ReverseMap();
                CreateMap<Infrastructure.Intern, InternModel>().ReverseMap();
                CreateMap<Infrastructure.Department, DepartmentModel>().ReverseMap();
                CreateMap<Infrastructure.Organization, OrganizationModel>().ReverseMap();
                CreateMap<Infrastructure.Training, TrainingModel>().ReverseMap();
                CreateMap<Infrastructure.Point, PointModel>().ReverseMap();
                CreateMap<Infrastructure.EventType, EventTypeModel>().ReverseMap();
                CreateMap<Infrastructure.Activity, ActivityModel>().ReverseMap();

                CreateMap<Infrastructure.Point, PointListModel>().ReverseMap();

                CreateMap<Infrastructure.User, InternListModel>();
                CreateMap<Infrastructure.Organization, InternListModel>();
                CreateMap<Infrastructure.Department, InternListModel>();
                CreateMap<Infrastructure.Training, InternListModel>();
                CreateMap<Infrastructure.Intern, InternListModel>()
                    .IncludeMembers(s => s.Mentor,
                                    s => s.Training,
                                    s => s.Organization,
                                    s => s.Department)
                    .ForMember(src => src.FullName, opt
                        => opt.MapFrom(dest => dest.FirstName + " " + dest.LastName))
                    .ForMember(src => src.Mentor, opt
                        => opt.MapFrom(dest => dest.Mentor.FirstName + " " + dest.Mentor.LastName))
                    .ReverseMap();
            }
        }
    }
}