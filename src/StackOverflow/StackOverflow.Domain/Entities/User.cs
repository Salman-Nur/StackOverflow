using StackOverflow.Domain.Entities;

namespace StackOverflow.Domain.Entities
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public int? Questions { get; set; }
        public int? Answers { get; set; }
    }
}