using AutoMapper;
using Moq;
using BinBetter.Api.Features.Users;

namespace BinBetter.Test.Mocks
{
    public class MockMappingProfiles
    {
        public static IMapper Get()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
              cfg =>
              {
                  cfg.AddProfile(new MappingProfiles());
              });

            IMapper mapper = new Mapper(mapperConfig);

            return mapper;
        }
    }
}
