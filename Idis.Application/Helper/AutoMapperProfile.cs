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
                CreateMap<UserModel, Infrastructure.User>().ReverseMap();
                CreateMap<EventModel, Infrastructure.Event>().ReverseMap();
                CreateMap<QuestionModel, Infrastructure.Question>().ReverseMap();
                CreateMap<InternModel, Infrastructure.Intern>().ReverseMap();
                CreateMap<DepartmentModel, Infrastructure.Department>().ReverseMap();
                CreateMap<OrganizationModel, Infrastructure.Organization>().ReverseMap();
                CreateMap<TrainingModel, Infrastructure.Training>().ReverseMap();
                CreateMap<PointModel, Infrastructure.Point>().ReverseMap();
                CreateMap<EventTypeModel, Infrastructure.EventType>().ReverseMap();
                CreateMap<ActivityModel, Infrastructure.Activity>().ReverseMap();

                CreateMap<PointListModel, Infrastructure.Point>().ReverseMap();
            }
        }
    }
}