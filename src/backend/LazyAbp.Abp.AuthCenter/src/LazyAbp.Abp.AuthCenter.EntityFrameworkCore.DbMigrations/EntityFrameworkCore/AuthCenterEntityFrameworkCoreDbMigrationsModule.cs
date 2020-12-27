using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    [DependsOn(
        typeof(AuthCenterEntityFrameworkCoreModule)
        )]
    public class AuthCenterEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AuthCenterMigrationsDbContext>();
        }
    }
}
