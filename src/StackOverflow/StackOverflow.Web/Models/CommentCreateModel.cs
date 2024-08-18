using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Domain.Entities;
using StackOverflow.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackOverflow.Web.Models
{
    public class CommentCreateModel
    {
        private ILifetimeScope _scope;
        private ICommentManagementService _commentManagementService;

        [Required(ErrorMessage = "Comment text required")]
        public string CommentText { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string UserName { get; set; }


        public CommentCreateModel() { }

        public CommentCreateModel(ICommentManagementService commentManagementService)
        {
            _commentManagementService = commentManagementService;

        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _commentManagementService = _scope.Resolve<ICommentManagementService>();
        }

        public async Task CreateCommentAsync()
        {
            await _commentManagementService.CreateCommentAsync(CommentText, UserId, QuestionId, UserName);
        }
    }
}
