using Microsoft.EntityFrameworkCore;
using StackOverflow.Application;
using StackOverflow.Infrastructure.Repositories;

namespace StackOverflow.Infrastructure
{
	public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{
        public IQuestionRepository QuestionRepository { get; private set; }
        public IAnswerRepository AnswerRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public IProfileRepository ProfileRepository { get; private set; }
        public IVoteRepository VoteRepository { get; private set; }



        public ApplicationUnitOfWork(
            IQuestionRepository questionRepository, 
            IAnswerRepository answerRepository, 
            ICommentRepository commentRepository,
            ITagRepository tagRepository, 
            IProfileRepository profileRepository, 
            IVoteRepository voteRepository,
            IApplicationDbContext dbContext) : base((DbContext)dbContext)
		{
            QuestionRepository = questionRepository;
            AnswerRepository = answerRepository;
            CommentRepository = commentRepository;
            TagRepository = tagRepository;
            ProfileRepository = profileRepository;
            VoteRepository = voteRepository;
		}
    }
}
