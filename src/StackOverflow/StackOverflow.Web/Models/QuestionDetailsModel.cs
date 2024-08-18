using Autofac;
using AutoMapper;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;

namespace StackOverflow.Web.Models
{
    public class QuestionDetailsModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public string CommentText { get; set; }
        public IList<Answer> Answers { get; set; } = new List<Answer>();
        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public string TagName { get; set; }
        public int TotalVote {  get; set; }

        private IQuestionManagementService _questionManagementService;
        private IAnswerManagementService _answerManagementService;
        private ICommentManagementService _commentManagementService;
        private IVoteManagementService _voteManagementService;
        private IMapper _mapper;

        public QuestionDetailsModel()
        {

        }

        public QuestionDetailsModel(IQuestionManagementService questionManagementService, 
            IAnswerManagementService answerManagementService, 
            ICommentManagementService commentManagementService, 
            IVoteManagementService voteManagementService, IMapper mapper)
        {
            _questionManagementService = questionManagementService;
            _answerManagementService = answerManagementService;
            _commentManagementService = commentManagementService;
            _voteManagementService = voteManagementService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _questionManagementService = scope.Resolve<IQuestionManagementService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public async Task LoadAsync(Guid id)
        {
            Question question = await _questionManagementService.GetQuestionAsync(id);
            if (question != null)
            {
                _mapper.Map(question, this);
            }
        }

        public async Task LoadAnswerAsync(Guid questionId)
        {
            var answers = await _answerManagementService.GetAnswersByQuestionIdAsync(questionId);
            for (int i = 0; i < answers.Count(); i++)
            {
                Answers.Add(answers[i]);
            }
        }

        public async Task LoadCommentAsync(Guid questionId)
        {
            var comments = await _commentManagementService.GetCommentsByQuestionIdAsync(questionId);
            for (int i = 0; i < comments.Count(); i++)
            {
                Comments.Add(comments[i]);
            }
        }

        public async Task LoadVoteAsync(Guid questionId)
        {
            var votes = await _voteManagementService.GetVotesByQuestionIdAsync(questionId);
            TotalVote = 0;
            foreach (var vote in votes)
            {
                if (vote.VoteType == 1)
                {
                    TotalVote++;
                }
                else
                {
                    TotalVote--;
                }
            }
        }
    }
}
