using AutoMapper;
using BinBetter.Api.Data.Domain;

namespace BinBetter.Api.Features.Users
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserModel>(MemberList.None).ReverseMap();
        }
    }
}
