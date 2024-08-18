using Autofac;
using StackOverflow.Web.Models;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegistrationModel>().AsSelf();
        builder.RegisterType<LoginModel>().AsSelf();
		builder.RegisterType<QuestionCreateModel>().AsSelf();
        builder.RegisterType<QuestionListModel>().AsSelf();
        builder.RegisterType<QuestionDetailsModel>().AsSelf();
        builder.RegisterType<AnswerCreateModel>().AsSelf();
        builder.RegisterType<TagListModel>().AsSelf();
        builder.RegisterType<ProfileEditModel>().AsSelf();
        builder.RegisterType<ProfileViewModel>().AsSelf();
        builder.RegisterType<MyQuestionListModel>().AsSelf();
        builder.RegisterType<CommentCreateModel>().AsSelf();
        builder.RegisterType<VoteModel>().AsSelf();
    }
}