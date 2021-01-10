using EasyAbp.IdentityServerAdmin.ApiResources;
using EasyAbp.IdentityServerAdmin.ApiResources.Dtos;
using LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer
{
    [RemoteService(Name = IdentityServerRemoteServiceConsts.RemoteServiceName)]
    [Area("identityServer")]
    [Route("api/identity-server/apiResource")]
    public class ApiResourceController : AbpController, IApiResourceAppService
    {
        private readonly IApiResourceAppService _apiResourceAppService;

        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            this._apiResourceAppService = apiResourceAppService;
        }

        [HttpPost]
        public async Task<ApiResourceDto> CreateAsync(CreateApiResourceInputDto input)
        {
            return await this._apiResourceAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await this._apiResourceAppService.DeleteAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ApiResourceDto> UpdateAsync(Guid id, UpdateApiResourceInputDto input)
        {
            return await this._apiResourceAppService.UpdateAsync(id, input);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ApiResourceDto> GetAsync(Guid id)
        {
            return await this._apiResourceAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<ApiResourceDto>> GetListAsync(GetApiResourceListInput input)
        {
            return await this._apiResourceAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("scopes")]
        public async Task AddScopesAsync(SharedApiScopeDto dto)
        {
            await this._apiResourceAppService.AddScopesAsync(dto);
        }

        [HttpGet]
        [Route("{id}/scopes")]
        public async Task<List<SharedApiScopeDto>> GetScopesAsync(Guid id)
        {
            return await this._apiResourceAppService.GetScopesAsync(id);
        }

        [HttpDelete]
        [Route("{id}/scopes")]
        public async Task DeleteScopesAsync(Guid id, string name)
        {
            await this._apiResourceAppService.DeleteScopesAsync(id, name);
        }

        [HttpPut]
        [Route("scopes")]
        public async Task UpdateScopesAsync(UpdateScopeInputDo input)
        {
            await this._apiResourceAppService.UpdateScopesAsync(input);
        }
    }
}
