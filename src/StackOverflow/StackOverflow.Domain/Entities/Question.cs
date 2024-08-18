using StackOverflow.Domain.Entities;
using System.ComponentModel;

namespace StackOverflow.Domain.Entities
{
    public class Question : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Tag { get; set; }
        public Guid UserId { get; set; }
    }
}
