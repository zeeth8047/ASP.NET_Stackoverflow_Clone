using AutoMapper;
using StackOverflow.Infrastructure.Entities.Membership;
using StackOverflow.Membership.DTOs;

namespace StackOverflow.Membership.Profiles
{
    public class MembershipProfile : Profile
    {
        public MembershipProfile()
        {
            CreateMap<UserBasicInfoDto, ApplicationUser>().ReverseMap();
        }
    }
}