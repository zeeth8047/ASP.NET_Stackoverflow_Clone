using Autofac;
using StackOverflow.Web.Areas.MyProfile.Models;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.Account;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //Home Models 
        builder.RegisterType<AllPostModel>().AsSelf();
        builder.RegisterType<PostDetailsModel>().AsSelf();
        builder.RegisterType<UpdateCommentModel>().AsSelf();

        //Account Models
        builder.RegisterType<RegisterModel>().AsSelf();
        builder.RegisterType<LoginModel>().AsSelf();

        //Post Models
        builder.RegisterType<CreatePostModel>().AsSelf();
        builder.RegisterType<UpdatePostModel>().AsSelf();
        builder.RegisterType<ListPostModel>().AsSelf();

        //Comment Models
        builder.RegisterType<CreateCommentModel>().AsSelf();        
        
        //Vote
        builder.RegisterType<VoteModel>().AsSelf();

        base.Load(builder);
    }
}