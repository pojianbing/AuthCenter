using LazyAbp.Abp.AuthCenter.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace LazyAbp.Abp.AuthCenter
{
    [DependsOn(
        typeof(AuthCenterEntityFrameworkCoreTestModule)
        )]
    public class AuthCenterDomainTestModule : AbpModule
    {

    }
}