using AnimalsShelterBackend.Domain.Contributors;
using Core.Base.Services;
using Core.Requests;
using Core.Requests.Contributors;
using Core.Responses.Contributors;
using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Contributors
{
	public class ContributorService: BaseService<Contributor>, IContributorsService
	{
		private readonly IContributorsRepository _repository;

		public ContributorService(IContributorsRepository repository): base(repository)
		{
			_repository = repository;
		}

		public override async Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var contributor = await GetByGuidAsync(id, CancellationToken.None);
			if (contributor == null)
				return new UpdateContributorResponse() { IsSuccess = false, Message = "Запись не найдена" };
			var contributorRequest = (UpdateContributorRequest)request;
			contributor.Name = contributorRequest.Name ?? contributor.Name;
			contributor.ContributorType = contributorRequest.ContributorType ?? contributor.ContributorType;
			await base.UpdateAsync(id, request);
			return new UpdateContributorResponse() { IsSuccess = true };
		}
	}
}
