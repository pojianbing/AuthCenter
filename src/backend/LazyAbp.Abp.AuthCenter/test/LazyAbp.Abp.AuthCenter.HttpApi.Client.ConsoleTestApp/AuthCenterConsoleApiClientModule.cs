using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace LazyAbp.Abp.AuthCenter.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(AuthCenterHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class AuthCenterConsoleApiClientModule : AbpModule
    {
        
    }
}
