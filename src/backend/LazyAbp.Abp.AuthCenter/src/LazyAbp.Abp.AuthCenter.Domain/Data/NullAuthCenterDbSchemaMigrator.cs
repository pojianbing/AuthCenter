using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LazyAbp.Abp.AuthCenter.Data
{
    /* This is used if database provider does't define
     * IAuthCenterDbSchemaMigrator implementation.
     */
    public class NullAuthCenterDbSchemaMigrator : IAuthCenterDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}