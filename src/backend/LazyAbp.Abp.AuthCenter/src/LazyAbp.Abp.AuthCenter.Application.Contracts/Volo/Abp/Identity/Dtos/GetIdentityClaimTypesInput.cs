using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity.Dtos
{
	public class GetIdentityClaimTypesInput : PagedAndSortedResultRequestDto
	{
		public string Filter { get; set; }

	}
}
