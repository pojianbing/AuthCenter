using EasyAbp.IdentityServerAdmin.IdentityResources;
using EasyAbp.IdentityServerAdmin.IdentityResources.Dtos;
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
    [Route("api/identity-server/identityResource")]
    public class IdentityResourceController : AbpController, IIdentityResourceAppService
    {
        private readonly IIdentityResourceAppService _identityResourceAppService;

        public IdentityResourceController(IIdentityResourceAppService identityResourceAppService)
        {
            this._identityResourceAppService = identityResourceAppService;
        }

        [HttpPost]
        public async Task<IdentityResourceDto> CreateAsync(CreateIdentityResourceInputDto input)
        {
            return await this._identityResourceAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await this._identityResourceAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IdentityResourceDto> GetAsync(Guid id)
        {
            return await this._identityResourceAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<IdentityResourceDto>> GetListAsync(GetIdentityResourceListInputDto input)
        {
            return await this._identityResourceAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IdentityResourceDto> UpdateAsync(Guid id, UpdateIdentityResourceInputDto input)
        {
            return await this._identityResourceAppService.UpdateAsync(id, input);
        }
    }
}
