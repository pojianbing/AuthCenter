using EasyAbp.IdentityServerAdmin.PersistedGrants;
using EasyAbp.IdentityServerAdmin.PersistedGrants.Dtos;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/identity-server/persistedGrant")]
    public class PersistedGrantController : AbpController, IPersistedGrantAppService
    {
        private readonly IPersistedGrantAppService _persistedGrantService;

        public PersistedGrantController(IPersistedGrantAppService persistedGrantService)
        {
            this._persistedGrantService = persistedGrantService;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await this._persistedGrantService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PersistedGrantDto> GetAsync(Guid id)
        {
            return await this._persistedGrantService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<PersistedGrantDto>> GetListAsync(GetPersistedGrantListInputDto input)
        {
            return await this._persistedGrantService.GetListAsync(input);
        }
    }
}
