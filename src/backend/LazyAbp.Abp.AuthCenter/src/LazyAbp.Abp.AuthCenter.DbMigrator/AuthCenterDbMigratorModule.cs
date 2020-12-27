using LazyAbp.Abp.AuthCenter.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace LazyAbp.Abp.AuthCenter.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AuthCenterEntityFrameworkCoreDbMigrationsModule),
        typeof(AuthCenterApplicationContractsModule)
        )]
    public class AuthCenterDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
