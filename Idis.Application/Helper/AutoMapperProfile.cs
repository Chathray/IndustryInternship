using AutoMapper;
using Idis.Infrastructure;
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
                CreateMap<UserModel, User>().ReverseMap();
                CreateMap<EventModel, Event>().ReverseMap();
                CreateMap<QuestionModel, Question>().ReverseMap();
                CreateMap<InternModel, Intern>().ReverseMap();
                CreateMap<DepartmentModel, Department>().ReverseMap();
                CreateMap<OrganizationModel, Organization>().ReverseMap();
                CreateMap<TrainingModel, Training>().ReverseMap();
                CreateMap<PointModel, Point>().ReverseMap();
                CreateMap<EventTypeModel, EventType>().ReverseMap();
                CreateMap<ActivityModel, Activity>().ReverseMap();

                CreateMap<PointListModel, Point>().ReverseMap();
            }
        }
    }
}