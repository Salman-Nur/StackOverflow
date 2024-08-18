using Autofac;
using AutoMapper;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;

namespace StackOverflow.Web.Models
{
    public class VoteModel
    {
        private ILifetimeScope _scope;
        private IVoteManagementService _voteManagementService;

		public Guid UserId { get; set; }
        public int VoteType { get; set; }
        public Guid QuestionId { get; set; }

		public VoteModel() { }

        public VoteModel(IVoteManagementService voteManagementService)
        {
            _voteManagementService = voteManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _voteManagementService = _scope.Resolve<IVoteManagementService>();
        }

        public async Task CreateVoteAsync()
        {
            await _voteManagementService.CreateVoteAsync(QuestionId, UserId, VoteType);
        }
    }
}
