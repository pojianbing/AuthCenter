using LazyAbp.Abp.AuthCenter.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity.Dtos;

namespace Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityRoleAppService), 
        typeof(AuthCenterIdentityRoleAppService),
        typeof(IHelloIdentityRoleAppService),
        typeof(AuthCenterIdentityRoleAppService))]
    public class AuthCenterIdentityRoleAppService : IdentityRoleAppService, IHelloIdentityRoleAppService
    {
        private IStringLocalizer<AuthCenterResource> _localizer;
        protected OrganizationUnitManager OrgManager { get; }
        public AuthCenterIdentityRoleAppService(IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
            IStringLocalizer<AuthCenterResource> localizer,
            OrganizationUnitManager orgManager) : base(roleManager, roleRepository)
        {
            _localizer = localizer;
            OrgManager = orgManager;
        }

        [Authorize(AuthCenterIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return OrgManager.AddRoleToOrganizationUnitAsync(roleId, ouId);
        }

        [Authorize(IdentityPermissions.Roles.Create)]
        [Authorize(AuthCenterIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            var role = await base.CreateAsync(
                ObjectMapper.Map<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>(input)
            );
            if (input.OrgId.HasValue)
            {
                await OrgManager.AddRoleToOrganizationUnitAsync(role.Id,input.OrgId.Value);
            }
            return role;
        }
    }
}
