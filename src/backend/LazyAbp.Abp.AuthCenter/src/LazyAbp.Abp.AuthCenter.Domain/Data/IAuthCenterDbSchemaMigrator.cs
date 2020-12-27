using System.Threading.Tasks;

namespace LazyAbp.Abp.AuthCenter.Data
{
    public interface IAuthCenterDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
