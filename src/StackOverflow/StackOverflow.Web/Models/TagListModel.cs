using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StackOverflow.Application.Features.Training.Services;
using StackOverflow.Infrastructure;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class TagListModel
    {
        private ILifetimeScope _scope;
        private ITagManagementService _tagManagementService;
        public TagListModel()
        {
        }
        public TagListModel(ITagManagementService tagManagementService)
        {
            _tagManagementService = tagManagementService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _tagManagementService = _scope.Resolve<ITagManagementService>();
        }
        public async Task<object> GetPagedTagsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _tagManagementService.GetPagedTagsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "TagName" })
                );

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                    HttpUtility.HtmlEncode(record.TagName),
                    record.QuestionId.ToString(),
                    record.Id.ToString()
                        }
                ).ToArray()
            };
        }
    }
}
