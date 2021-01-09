using EasyAbp.IdentityServerAdmin.ApiResources;
using EasyAbp.IdentityServerAdmin.ApiResources.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using EasyAbp.IdentityServerAdmin.Permissions;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using LazyAbp.Abp.AuthCenter.IdentityServer;
using LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.ApiResources.Dtos;
using Volo.Abp;

namespace EasyAbp.IdentityServerAdmin.Application.EasyAbp.IdentityServerAdmin.ApiResources
{
    [Authorize(IdentityServerAdminPermissions.ApiResource.Default)]
    public class ApiResourceAppService : ReadOnlyAppService<ApiResource, ApiResourceDto, Guid, GetApiResourceListInput>, IApiResourceAppService
    {
        private readonly IApiResourceRepository _repository;
        private readonly IRepository<SharedApiScope> _apiScopeRepository;
        private readonly IRepository<SharedApiScopeClaim> _apiScopeClaimRepository;

        public ApiResourceAppService(
            IApiResourceRepository repository,
            IRepository<SharedApiScope> apiScopeRepository,
            IRepository<SharedApiScopeClaim> apiScopeClaimRepository)
            : base((IReadOnlyRepository<ApiResource, Guid>)repository)
        {
            _repository = repository;
            _apiScopeRepository = apiScopeRepository;
            _apiScopeClaimRepository = apiScopeClaimRepository;
        }

        public override async Task<PagedResultDto<ApiResourceDto>> GetListAsync(GetApiResourceListInput input)
        {

            var totalCount = await _repository.GetCountAsync();
            var entities = await _repository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, null, includeDetails: true);
            return new PagedResultDto<ApiResourceDto>(totalCount, entities.Select(MapToGetListOutputDto).ToList());

        }

        public override async Task<ApiResourceDto> GetAsync(Guid id)
        {
            var apiResource = await GetEntityByIdAsync(id);
            return MapToGetListOutputDto(apiResource);
        }


        [Authorize(IdentityServerAdminPermissions.ApiResource.Create)]
        public async Task<ApiResourceDto> CreateAsync(CreateApiResourceInputDto input)
        {

            var nameExist = await _repository.CheckNameExistAsync(input.Name);

            if (nameExist)
            {
                throw new ApiResourceNameNowAllowedDuplicateException(input.Name);
            }

            var apiResource = new ApiResource(GuidGenerator.Create(), input.Name, input.DisplayName, input.Description);

            // 新建时不创建scope
            apiResource.RemoveAllScopes();

            input.UserClaims.ForEach(x => apiResource.AddUserClaim(x));
            apiResource = await _repository.InsertAsync(apiResource);
            return MapToGetOutputDto(apiResource);
        }

        [Authorize(IdentityServerAdminPermissions.ApiResource.Update)]
        public async Task<ApiResourceDto> UpdateAsync(Guid id, UpdateApiResourceInputDto input)
        {
            var nameExist = await _repository.CheckNameExistAsync(input.Name, id);

            if (nameExist)
            {
                throw new ApiResourceNameNowAllowedDuplicateException(input.Name);
            }

            // 新生成密钥为明文,需要sha256
            input.TryEncryptSecrets();

            var apiResource = await _repository.FindAsync(id, true);
            apiResource = ObjectMapper.Map<UpdateApiResourceInputDto, ApiResource>(input, apiResource);

            apiResource.RemoveAllUserClaims();
            input.UserClaims.ForEach(claim =>
            {
                apiResource.AddUserClaim(claim);
            });

            apiResource = await _repository.UpdateAsync(apiResource);
            return MapToGetListOutputDto(apiResource);
        }

        [Authorize(IdentityServerAdminPermissions.ApiResource.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var apiResource = await _repository.FindAsync(id);
            await _repository.DeleteAsync(apiResource);
        }

        [Authorize(IdentityServerAdminPermissions.ApiResource.Update)]
        public async Task<List<SharedApiScopeDto>> GetScopesAsync(Guid id)
        {
            var scopes = _apiScopeRepository.AsQueryable().Where(e => e.ApiResourceId == id).ToList();
            return ObjectMapper.Map<List<SharedApiScope>, List<SharedApiScopeDto>>(scopes);
        }

        [Authorize(IdentityServerAdminPermissions.ApiResource.Update)]
        public async Task DeleteScopesAsync(Guid id, string name)
        {
            await _apiScopeClaimRepository.DeleteAsync(e => e.ApiResourceId == id && e.Name == name);
            await _apiScopeRepository.DeleteAsync(e => e.ApiResourceId == id && e.Name == name);
        }


        [Authorize(IdentityServerAdminPermissions.ApiResource.Update)]
        public async Task AddScopesAsync(SharedApiScopeDto dto)
        {
            // scope需要保持系统唯一，否则报错
            var exist = _apiScopeRepository.Count(e => e.Name == dto.Name) > 0;
            if (exist) throw new UserFriendlyException("已存在同名scope，scope需要保证跨资源唯一");

            var dbModel = this.ObjectMapper.Map<SharedApiScopeDto, SharedApiScope>(dto);
            await _apiScopeRepository.InsertAsync(dbModel);
        }

        [Authorize(IdentityServerAdminPermissions.ApiResource.Update)]
        public async Task UpdateScopesAsync(UpdateScopeInputDo input)
        {
            if (input.BeforeName != input.Scope.Name)
            {
                // scope需要保持系统唯一，否则报错
                var exist = _apiScopeRepository.Count(e => e.Name == input.Scope.Name) > 0;
                if (exist) throw new UserFriendlyException("已存在同名scope，scope需要保证跨资源唯一");
            }

            // 先删除后插入
            await this.DeleteScopesAsync(input.BeforeApiResourceId, input.BeforeName);
            await this.AddScopesAsync(input.Scope);
        }
    }
}
