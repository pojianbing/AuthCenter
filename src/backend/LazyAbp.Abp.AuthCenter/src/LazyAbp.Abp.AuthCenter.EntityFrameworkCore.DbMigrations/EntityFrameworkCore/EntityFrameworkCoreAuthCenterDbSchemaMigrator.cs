using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LazyAbp.Abp.AuthCenter.Data;
using Volo.Abp.DependencyInjection;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    public class EntityFrameworkCoreAuthCenterDbSchemaMigrator
        : IAuthCenterDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAuthCenterDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AuthCenterMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AuthCenterMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}