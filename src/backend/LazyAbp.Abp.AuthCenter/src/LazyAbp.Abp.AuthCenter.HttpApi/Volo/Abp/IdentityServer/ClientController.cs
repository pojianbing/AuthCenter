using EasyAbp.IdentityServerAdmin.Clients;
using EasyAbp.IdentityServerAdmin.Clients.Dtos;
using LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.Clients.Dtos;
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
    [Route("api/identity-server/client")]
    public class ClientController : AbpController, IClientAppService
    {
        private readonly IClientAppService _clientAppService;

        public ClientController(IClientAppService clientAppService)
        {
            this._clientAppService = clientAppService;
        }

        [HttpPost]
        public async Task<ClientDto> CreateAsync(CreateClientInputDto input)
        {
            return await this._clientAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ClientDto> UpdateAsync(Guid id, UpdateClientInputDto input)
        {
            return await this._clientAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await this._clientAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("searchConsts")]
        public List<string> SearchConsts(SearchConstsInputDto input)
        {
            return this._clientAppService.SearchConsts(input);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ClientDto> GetAsync(Guid id)
        {
            return await this._clientAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListInputDto input)
        {
            return await this._clientAppService.GetListAsync(input);
        }
    }
}
