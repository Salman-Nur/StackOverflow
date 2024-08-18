namespace StackOverflow.Domain.Entities
{
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string UserName { get; set; }
    }
}