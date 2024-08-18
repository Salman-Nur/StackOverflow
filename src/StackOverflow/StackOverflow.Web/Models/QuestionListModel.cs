using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Infrastructure;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class QuestionListModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;
        public QuestionListModel()
        {
        }
        public QuestionListModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }
        public async Task<object> GetPagedQuestionsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionManagementService.GetPagedQuestionsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Title", "Body", "Tag" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.Title),
                            HttpUtility.HtmlEncode(record.Body),
							HttpUtility.HtmlEncode(record.Tag),
							record.Id.ToString()
                        }
                        ).ToArray()
            };
        }
    }
}
