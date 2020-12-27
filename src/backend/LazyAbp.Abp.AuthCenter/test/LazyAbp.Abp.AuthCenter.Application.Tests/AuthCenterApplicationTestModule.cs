using Volo.Abp.Modularity;

namespace LazyAbp.Abp.AuthCenter
{
    [DependsOn(
        typeof(AuthCenterApplicationModule),
        typeof(AuthCenterDomainTestModule)
        )]
    public class AuthCenterApplicationTestModule : AbpModule
    {

    }
}