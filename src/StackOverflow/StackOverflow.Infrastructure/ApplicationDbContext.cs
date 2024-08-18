using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverflow.DataSeeder;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.DataSeeder;
using StackOverflow.Infrastructure.Membership;
using System.Reflection.Emit;

namespace StackOverflow.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>,
        IApplicationDbContext
    {
		private readonly string _connectionString;
		private readonly string _migrationAssembly;

		public ApplicationDbContext(string connectionString, string migrationAssembly)
		{
			_connectionString = connectionString;
			_migrationAssembly = migrationAssembly;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString,
					x => x.MigrationsAssembly(_migrationAssembly));
			}

			base.OnConfiguring(optionsBuilder);
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(UserProfileSeed.UserProfiles());
            builder.Entity<ApplicationUser>().HasData(UserSeed.Users());
            builder.Entity<ApplicationUserClaim>().HasData(UserClaimSeed.Claims());

            base.OnModelCreating(builder);
        }

        public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}