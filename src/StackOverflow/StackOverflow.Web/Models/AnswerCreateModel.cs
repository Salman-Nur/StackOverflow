using Autofac;
using AutoMapper;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackOverflow.Web.Models
{
    public class AnswerCreateModel
    {
        private ILifetimeScope _scope;
        private IAnswerManagementService _answerManagementService;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid QuestionId { get; set; }

        [Required]
        public string AnswerText { get; set; }


		public AnswerCreateModel() { }

        public AnswerCreateModel(IAnswerManagementService answerManagementService)
        {
            _answerManagementService = answerManagementService;

        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _answerManagementService = _scope.Resolve<IAnswerManagementService>();
        }

        public async Task CreateAnswerAsync()
        {
            await _answerManagementService.CreateAnswerAsync(AnswerText, QuestionId, UserId, UserName);
        }
    }
}
