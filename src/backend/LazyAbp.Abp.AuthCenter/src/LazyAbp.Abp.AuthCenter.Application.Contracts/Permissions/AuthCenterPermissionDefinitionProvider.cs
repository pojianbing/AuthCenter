using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LazyAbp.Abp.AuthCenter.Permissions
{
    public class AuthCenterPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(AuthCenterPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(AuthCenterPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuthCenterResource>(name);
        }
    }
}
