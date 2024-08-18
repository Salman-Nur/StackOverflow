using Autofac;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application
{
    public class ApplicationModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<QuestionManagementService>().As<IQuestionManagementService>()
				.InstancePerLifetimeScope();

            builder.RegisterType<AnswerManagementService>().As<IAnswerManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TagManagementService>().As<ITagManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProfileManagementService>().As<IProfileManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VoteManagementService>().As<IVoteManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentManagementService>().As<ICommentManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueueService>().As<IQueueService>()
                .InstancePerLifetimeScope();
        }
	}
}
