using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class AuthCenterMigrationsDbContextFactory : IDesignTimeDbContextFactory<AuthCenterMigrationsDbContext>
    {
        public AuthCenterMigrationsDbContext CreateDbContext(string[] args)
        {
            AuthCenterEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AuthCenterMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new AuthCenterMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LazyAbp.Abp.AuthCenter.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
