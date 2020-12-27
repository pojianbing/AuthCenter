using Volo.Abp.Settings;

namespace LazyAbp.Abp.AuthCenter.Settings
{
    public class AuthCenterSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AuthCenterSettings.MySetting1));
        }
    }
}
