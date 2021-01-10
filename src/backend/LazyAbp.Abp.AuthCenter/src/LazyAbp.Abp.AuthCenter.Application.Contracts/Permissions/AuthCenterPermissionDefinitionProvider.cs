using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace LazyAbp.Abp.AuthCenter.Permissions
{
    public class AuthCenterPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroup(IdentityPermissions.GroupName);

            var ouPermission = identityGroup.AddPermission(AuthCenterIdentityPermissions.OrganitaionUnits.Default, IdentityL("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(AuthCenterIdentityPermissions.OrganitaionUnits.Create, IdentityL("Permission:Create"));
            ouPermission.AddChild(AuthCenterIdentityPermissions.OrganitaionUnits.Update, IdentityL("Permission:Edit"));
            ouPermission.AddChild(AuthCenterIdentityPermissions.OrganitaionUnits.Delete, IdentityL("Permission:Delete"));

            var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            userPermission?.AddChild(AuthCenterIdentityPermissions.Users.DistributionOrganizationUnit, IdentityL("Permission:DistributionOrganizationUnit"));

            var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            rolePermission?.AddChild(AuthCenterIdentityPermissions.Roles.AddOrganizationUnitRole, IdentityL("Permission:AddOrganizationUnitRole"));

            //Claim
            var claimPermission = identityGroup.AddPermission(AuthCenterIdentityPermissions.ClaimTypes.Default, IdentityL("Permission:ClaimManagement"));
            claimPermission.AddChild(AuthCenterIdentityPermissions.ClaimTypes.Create, IdentityL("Permission:Create"));
            claimPermission.AddChild(AuthCenterIdentityPermissions.ClaimTypes.Update, IdentityL("Permission:Edit"));
            claimPermission.AddChild(AuthCenterIdentityPermissions.ClaimTypes.Delete, IdentityL("Permission:Delete"));

            //AuditLogging
            var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName);
            // 修复不显示中文的问题
            auditLogGroup.DisplayName = AuditLoggingL("Permission:AuditLogManagement");
            var aduditLogPermission = auditLogGroup.AddPermission(AuditLogPermissions.AuditLogs.Default, AuditLoggingL("Permission:AuditLogManagement"));
            aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Delete, AuditLoggingL("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuthCenterResource>(name);
        }

        private static LocalizableString IdentityL(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }

        private static LocalizableString AuditLoggingL(string name)
        {
            return LocalizableString.Create<AuditLoggingResource>(name);
        }
    }
}
