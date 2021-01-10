using EasyAbp.IdentityServerAdmin.ApiResources.Dtos;
using LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.IdentityServerAdmin.ApiResources
{
    public interface IApiResourceAppService : IReadOnlyAppService<ApiResourceDto, Guid, GetApiResourceListInput>
    {

        Task<ApiResourceDto> CreateAsync(CreateApiResourceInputDto input);
        Task<ApiResourceDto> UpdateAsync(Guid id,UpdateApiResourceInputDto input);
        Task DeleteAsync(Guid id);
        Task<List<SharedApiScopeDto>> GetScopesAsync(Guid id);
        Task AddScopesAsync(SharedApiScopeDto dto);
        Task DeleteScopesAsync(Guid id, string name);
        Task UpdateScopesAsync(UpdateScopeInputDo input);
    }
}
