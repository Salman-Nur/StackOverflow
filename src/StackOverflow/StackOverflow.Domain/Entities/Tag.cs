namespace StackOverflow.Domain.Entities
{
    public class Tag : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public Guid QuestionId { get; set; }
    }
}