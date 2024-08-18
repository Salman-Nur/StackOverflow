using Autofac;
using StackOverflow.Application;
using StackOverflow.Application.Utilities;
using StackOverflow.Infrastructure.Email;
using StackOverflow.Infrastructure.Membership;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure
{
	public class InfrastructureModule : Module
	{
		private readonly string _connectionString;
		private readonly string _migrationAssembly;
		public InfrastructureModule(string connectionString, string migrationAssembly)
		{
			_connectionString = connectionString;
			_migrationAssembly = migrationAssembly;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ApplicationDbContext>().AsSelf()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssembly)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssembly)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
				.InstancePerLifetimeScope();

            builder.RegisterType<HtmlEmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TokenService>().As<ITokenService>()
               .InstancePerLifetimeScope();

			builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<AnswerRepository>().As<IAnswerRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CommentRepository>().As<ICommentRepository>()
				.InstancePerLifetimeScope();

            builder.RegisterType<TagRepository>().As<ITagRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProfileRepository>().As<IProfileRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VoteRepository>().As<IVoteRepository>()
                .InstancePerLifetimeScope();

        }
	}
}
