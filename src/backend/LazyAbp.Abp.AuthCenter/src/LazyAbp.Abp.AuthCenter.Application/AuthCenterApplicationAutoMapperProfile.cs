using AutoMapper;
using EasyAbp.IdentityServerAdmin.ApiResources.Dtos;
using EasyAbp.IdentityServerAdmin.Clients.Dtos;
using EasyAbp.IdentityServerAdmin.IdentityResources.Dtos;
using EasyAbp.IdentityServerAdmin.PersistedGrants.Dtos;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Dtos;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Dtos;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;
using System.Linq;
using System.Collections.Generic;

namespace LazyAbp.Abp.AuthCenter
{
    public class AuthCenterApplicationAutoMapperProfile : Profile
    {
        public AuthCenterApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();

            CreateMap<IdentityUserOrgCreateDto, IdentityUserCreateDto>();
            CreateMap<IdentityUserOrgUpdateDto, IdentityUserUpdateDto>();

            CreateMap<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>();

            //AuditLog
            CreateMap<AuditLog, AuditLogDto>()
                .MapExtraProperties();

            CreateMap<EntityChange, EntityChangeDto>()
                .MapExtraProperties();

            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            CreateMap<AuditLogAction, AuditLogActionDto>();

            //Claim
            CreateMap<IdentityClaimType, ClaimTypeDto>().Ignore(x => x.ValueTypeAsString);
            CreateMap<IdentityUserClaim, IdentityUserClaimDto>();
            CreateMap<IdentityUserClaimDto, IdentityUserClaim>().Ignore(x => x.TenantId).Ignore(x => x.Id);
            CreateMap<IdentityRoleClaim, IdentityRoleClaimDto>();
            CreateMap<IdentityRoleClaimDto, IdentityRoleClaim>().Ignore(x => x.TenantId).Ignore(x => x.Id);
            CreateMap<CreateClaimTypeDto, IdentityClaimType>().Ignore(x => x.IsStatic).Ignore(x => x.Id);
            CreateMap<UpdateClaimTypeDto, IdentityClaimType>().Ignore(x => x.IsStatic).Ignore(x => x.Id);

            CreateMap<ApiResource, ApiResourceDto>()
                .ForMember(des => des.UserClaims,
                    opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)))
                ;
            CreateMap<ApiSecret, ApiSecretDto>().ReverseMap();
            CreateMap<ApiScopeDto, ApiScope>()
                .ForMember(des => des.UserClaims,
                    opt => opt.MapFrom((src, dest) =>
                    {
                        dest.UserClaims = new List<ApiScopeClaim>();
                        return "";
                    }));
            CreateMap<ApiScope, ApiScopeDto>()
                .ForMember(des => des.UserClaims,
                    opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type))
                );

            CreateMap<UpdateApiResourceInputDto, ApiResource>()
                .Ignore(des => des.UserClaims)
                .Ignore(des => des.Id)
                .Ignore(des => des.Properties)
                .Ignore(des => des.ExtraProperties)
                .Ignore(des => des.ConcurrencyStamp)
                .IgnoreFullAuditedObjectProperties();

            CreateMap<Client, ClientDto>()
                .ForMember(des => des.IdentityProviderRestrictions,
                    opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider))
                )
                .ForMember(des => des.PostLogoutRedirectUris,
                    opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.PostLogoutRedirectUri))
                )
                .ForMember(des => des.RedirectUris,
                    opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.RedirectUri))
                )
                .ForMember(des => des.AllowedCorsOrigins,
                    opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin))
                )
                .ForMember(des => des.AllowedGrantTypes,
                    opt => opt.MapFrom(src => src.AllowedGrantTypes.Select(x => x.GrantType))
                )
                .ForMember(des => des.AllowedScopes,
                    opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope))
                )
                ;
            CreateMap<ClientSecret, ClientSecretDto>().ReverseMap();
            CreateMap<ClientProperty, ClientPropertyDto>().ReverseMap();
            CreateMap<ClientClaim, ClientClaimDto>().ReverseMap();

            CreateMap<UpdateClientInputDto, Client>()
                .Ignore(des => des.IdentityProviderRestrictions)
                .Ignore(des => des.PostLogoutRedirectUris)
                .Ignore(des => des.RedirectUris)
                .Ignore(des => des.AllowedCorsOrigins)
                .Ignore(des => des.AllowedGrantTypes)
                .Ignore(des => des.AllowedScopes)
                .Ignore(des => des.Id)
                .Ignore(des => des.Properties)
                .Ignore(des => des.ExtraProperties)
                .Ignore(des => des.ConcurrencyStamp)
                .IgnoreFullAuditedObjectProperties();

            CreateMap<IdentityResource, IdentityResourceDto>()
                 .ForMember(des => des.UserClaims,
                    opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)))
                 .ForMember(des => des.Properties,
                    opt => opt.MapFrom((src, dest) =>
                    {
                        if (src.Properties == null) return new List<ClientPropertyDto>();
                        return src.Properties.Select(e => new ClientPropertyDto { Key = e.Key, Value = e.Value }).ToList();
                    }));
            CreateMap<UpdateIdentityResourceInputDto, IdentityResource>()
                 .ForMember(des => des.Properties,
                    opt => opt.MapFrom((src, dest) =>
                    {
                        var result = new Dictionary<string, string>();
                        if (src.Properties == null) return result;

                        src.Properties.ForEach(e =>
                        {
                            if (!result.ContainsKey(e.Key))
                            {
                                result.Add(e.Key, e.Value);
                            }
                        });

                        return result;
                    }))
                .Ignore(des => des.Id)
                .Ignore(des => des.UserClaims)
                .Ignore(des => des.ExtraProperties)
                .Ignore(des => des.ConcurrencyStamp)
                .IgnoreFullAuditedObjectProperties();

            CreateMap<PersistedGrant, PersistedGrantDto>();
        }
    }
}
