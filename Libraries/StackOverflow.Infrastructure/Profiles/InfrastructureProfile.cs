using AutoMapper;
using StackOverflow.Infrastructure.BusinessObjects;
using StackOverflow.Infrastructure.Entities.Membership;
using PostEntity = StackOverflow.Infrastructure.Entities.Post;
using CommentEntity = StackOverflow.Infrastructure.Entities.Comment;
using VoteEntity = StackOverflow.Infrastructure.Entities.Vote;
using TagEntity = StackOverflow.Infrastructure.Entities.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<ApplicationMember, ApplicationUser>().ReverseMap();
            CreateMap<Post, PostEntity>().ReverseMap();
            CreateMap<Comment, CommentEntity>().ReverseMap();
            CreateMap<Vote, VoteEntity>().ReverseMap();
            CreateMap<Tag, TagEntity>().ReverseMap();
        }
    }
}
