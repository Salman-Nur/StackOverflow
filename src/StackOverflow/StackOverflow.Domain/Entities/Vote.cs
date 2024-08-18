namespace StackOverflow.Domain.Entities
{
    public class Vote : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public int VoteType { get; set; }
    }
}