using StackOverflow.Domain;
using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        ICommentRepository CommentRepository { get; }
        ITagRepository TagRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IVoteRepository VoteRepository { get; }
    }
}
