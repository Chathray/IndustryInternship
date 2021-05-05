using AutoMapper;
using Idis.Application;

namespace Idis.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, UserModel>();
        }
    }
}