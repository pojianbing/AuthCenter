using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace LazyAbp.Abp.AuthCenter.Web
{
    [Dependency(ReplaceServices = true)]
    public class AuthCenterBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AuthCenter";
    }
}
