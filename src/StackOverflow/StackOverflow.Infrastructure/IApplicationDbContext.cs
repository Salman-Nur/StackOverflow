using Microsoft.EntityFrameworkCore;
using StackOverflow.Domain.Entities;

namespace StackOverflow.Infrastructure
{
    public interface IApplicationDbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}