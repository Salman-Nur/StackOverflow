using StackOverflow.Domain.Entities;

namespace StackOverflow.Domain.Entities
{
    public class Answer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string AnswerText { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}