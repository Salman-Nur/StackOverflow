using StackOverflow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Features.Training.Services
{
	public class TagManagementService : ITagManagementService
	{
		private readonly IApplicationUnitOfWork _unitOfWork;
        public TagManagementService(IApplicationUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


        public async Task<(IList<Tag> records, int total, int totalDisplay)> 
			GetPagedTagsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
        {
            return await _unitOfWork.TagRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize);
        }
	}
}
