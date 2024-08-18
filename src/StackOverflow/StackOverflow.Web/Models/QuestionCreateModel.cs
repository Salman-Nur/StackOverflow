using Autofac;
using StackOverflow.Application.Features.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models
{
    public class QuestionCreateModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;

        public Guid Id { get; set; } = Guid.NewGuid();
		public Guid UserId { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Body required")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Tag name required")]
        public string Tag { get; set; }
        public Guid QuestionId { get; set; }

		public QuestionCreateModel() { }

        public QuestionCreateModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }
		public void ResolveDependency(ILifetimeScope scope)
		{
			_questionManagementService = scope.Resolve<IQuestionManagementService>();
		}

		internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        public async Task CreateQuestionAsync()
        {
            await _questionManagementService.CreateQuestionAsync(Id, Title, Body,Tag, UserId);
        }

        public async Task CreateTagAsync()
        {
            await _questionManagementService.CreateTagAsync(Tag, Id);
        }
    }
}
