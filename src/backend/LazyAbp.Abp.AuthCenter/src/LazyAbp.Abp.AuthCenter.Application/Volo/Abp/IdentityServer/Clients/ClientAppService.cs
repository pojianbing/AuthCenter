using EasyAbp.IdentityServerAdmin.Clients.Dtos;
using EasyAbp.IdentityServerAdmin.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.Clients;
using LazyAbp.Abp.AuthCenter.Extensions;
using LazyAbp.Abp.AuthCenter.IdentityServer;
using LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.Clients.Dtos;

namespace EasyAbp.IdentityServerAdmin.Clients
{
    [Authorize(IdentityServerAdminPermissions.Client.Default)]
    public class ClientAppService : ReadOnlyAppService<Client, ClientDto, Guid, GetClientListInputDto>, IClientAppService
    {
        private readonly IClientRepository _repository;

        private readonly IRepository<SharedIdentityResource, Guid> _identityResourcesRepository;
        private readonly IRepository<SharedApiScopes> _apiScopeRepository;

        public ClientAppService(
            IClientRepository repository,
            IRepository<SharedIdentityResource, Guid> identityResourcesRepository,
            IRepository<SharedApiScopes> apiScopeRepository
            ) : base((IReadOnlyRepository<Client, Guid>)repository)
        {
            _repository = repository;
            _identityResourcesRepository = identityResourcesRepository;
            _apiScopeRepository = apiScopeRepository;
        }

        protected override IQueryable<Client> CreateFilteredQuery(GetClientListInputDto input)
        {
            return base.ReadOnlyRepository
                .AsQueryable()
                .WhereIf(!input.Keywords.IsNullOrWhiteSpace(),
                         e => e.ClientId.Contains(input.Keywords) ||
                         e.ClientName.Contains(input.Keywords));
        }


        public override Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListInputDto input)
        {
            return base.GetListAsync(input);
        }

        public override Task<ClientDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        [Authorize(IdentityServerAdminPermissions.Client.Create)]
        public async Task<ClientDto> CreateAsync(CreateClientInputDto input)
        {
            var clientIdIsExist = await _repository.CheckClientIdExistAsync(input.ClientId.Trim());
            if (clientIdIsExist)
            {
                throw new ClientIdNotAllowedDuplicateException(input.ClientId);
            }

            var client = new Client(GuidGenerator.Create(), input.ClientId.Trim())
            {
                ClientName = input.ClientName?.Trim(),
                ClientUri = input.ClientUri?.Trim(),
                LogoUri = input.LogoUri?.Trim(),
                Description = input.Description?.Trim()
            };

            ConfigureClient(client, input.ClientType);
            client = await _repository.InsertAsync(client);
            return MapToGetOutputDto(client);
        }


        [Authorize(IdentityServerAdminPermissions.Client.Update)]
        public async Task<ClientDto> UpdateAsync(Guid id, UpdateClientInputDto input)
        {
            var clientIdIsExist = await _repository.CheckClientIdExistAsync(input.ClientId, id);
            if (clientIdIsExist)
            {
                throw new ClientIdNotAllowedDuplicateException(input.ClientId);
            }

            // 新生成密钥为明文,需要sha256
            input.TryEncryptSecrets();

            var client = await _repository.FindAsync(id, true);
            client = ObjectMapper.Map<UpdateClientInputDto, Client>(input, client);

            client.RemoveAllIdentityProviderRestrictions();
            input.IdentityProviderRestrictions
              .ForEach(x => client.AddIdentityProviderRestriction(x));

            client.RemoveAllPostLogoutRedirectUris();
            input.PostLogoutRedirectUris
              .ForEach(x => client.AddPostLogoutRedirectUri(x));

            client.RemoveAllRedirectUris();
            input.RedirectUris
              .ForEach(x => client.AddRedirectUri(x));

            client.RemoveAllCorsOrigins();
            input.AllowedCorsOrigins
              .ForEach(x => client.AddCorsOrigin(x));

            client.RemoveAllAllowedGrantTypes();
            input.AllowedGrantTypes
              .ForEach(x => client.AddGrantType(x));

            client.RemoveAllScopes();
            input.AllowedScopes
              .ForEach(x => client.AddScope(x));

            client.RemoveAllProperties();
            input.Properties
              .ForEach(x => client.AddProperty(x.Key, x.Value));

            client = await _repository.UpdateAsync(client);
            return MapToGetOutputDto(client);
        }
        [Authorize(IdentityServerAdminPermissions.Client.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var client = await _repository.FindAsync(id);

            await _repository.DeleteAsync(client);
        }

        public List<string> SearchConsts(SearchConstsInputDto input)
        {
            switch (input.Type)
            {
                case "scope":
                    return SearchScopes(input.Text, input.Limit);
                case "claim":
                    return SearchClaims(input.Text, input.Limit);
                case "grantType":
                    return SearchGrantTypes(input.Text, input.Limit);
                default:
                    return new List<string>();
            }
        }

        private List<string> SearchScopes(string scope, int limit = 0)
        {
            var identityResources = _identityResourcesRepository
                .WhereIf(!string.IsNullOrEmpty(scope), x => x.Name.Contains(scope))
                .TakeIf(x => x.Id, limit > 0, limit)
                .Select(x => x.Name).ToList();

            var apiScopes = _apiScopeRepository
                .WhereIf(!string.IsNullOrEmpty(scope), x => x.Name.Contains(scope))
                .TakeIf(x => x.ApiResourceId, limit > 0, limit)
                .Select(x => x.Name).ToList();

            var scopes = identityResources.Concat(apiScopes).TakeIf(x => x, limit > 0, limit).ToList();

            return scopes;
        }

        private List<string> SearchClaims(string claim, int limit = 0)
        {
            var filteredClaims = LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.Clients.ClientConsts.GetStandardClaims()
               .WhereIf(!string.IsNullOrWhiteSpace(claim), x => x.Contains(claim))
               .TakeIf(x => x, limit > 0, limit)
               .ToList();

            return filteredClaims;
        }

        private List<string> SearchGrantTypes(string grant, int limit = 0)
        {
            var filteredGrants = LazyAbp.Abp.AuthCenter.Volo.Abp.IdentityServer.Clients.ClientConsts.GetGrantTypes()
                .WhereIf(!string.IsNullOrWhiteSpace(grant), x => x.Contains(grant))
                .TakeIf(x => x, limit > 0, limit)
                .ToList();

            return filteredGrants;
        }

        private void ConfigureClient(Client client, ClientType clientType)
        {
            switch (clientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.Device:
                    ConfigureDevice(client);
                    break;
                case ClientType.WebServerSideRenderer:
                    ConfigureWebServiceSide(client);
                    break;
                case ClientType.Spa:
                    ConfigureWebSpa(client);
                    break;
                case ClientType.WebHybrid:
                    ConfigureWebHybrid(client);
                    break;
                case ClientType.Native:
                    ConfigureNative(client);
                    break;
                case ClientType.Machine:
                    ConfigureMachine(client);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ClientType));
            }
        }

        private void ConfigureClientDefaultUrls(Client client)
        {
            if (!string.IsNullOrEmpty(client.ClientUri))
            {
                client.AddCorsOrigin(client.ClientUri);
                client.AddRedirectUri(client.ClientUri);
                if (!client.PostLogoutRedirectUris.Any())
                {
                    client.AddPostLogoutRedirectUri(client.ClientUri);
                }
            }
        }

        private void ConfigureDevice(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.DeviceFlow);
            client.AddScope("openid");
            client.RequireClientSecret = false;
            client.AllowOfflineAccess = true;
        }

        private void ConfigureWebServiceSide(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.AuthorizationCode);
            client.AllowAccessTokensViaBrowser = false;
            client.RequireClientSecret = true;
            client.RequirePkce = true;
            client.AddScope("openid");
            client.AddScope("profile");
            ConfigureClientDefaultUrls(client);
        }

        private void ConfigureWebSpa(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.AuthorizationCode);
            client.RequireClientSecret = false;
            client.RequirePkce = true;
            client.AddScope("openid");
            client.AddScope("profile");
            ConfigureClientDefaultUrls(client);
        }
        private void ConfigureWebHybrid(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.Hybrid);
            client.AddScope("openid");
            client.AddScope("profile");
            ConfigureClientDefaultUrls(client);
        }

        private void ConfigureNative(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.AuthorizationCode);
            client.RequireClientSecret = false;
            client.AddScope("openid");
            client.AddScope("profile");
        }
        private void ConfigureMachine(Client client)
        {
            client.AddGrantType(IdentityServer4.Models.GrantType.ClientCredentials);
            client.AddScope("openid");
            client.RequireConsent = false;
            client.RequireClientSecret = true;
        }
    }
}
