using Microsoft.EntityFrameworkCore;
using LazyAbp.Abp.AuthCenter.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.EntityFrameworkCore.ValueConverters;
using Volo.Abp.EntityFrameworkCore.ValueComparers;
using System.Collections.Generic;
using LazyAbp.Abp.AuthCenter.IdentityServer;
using Volo.Abp.IdentityServer;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See AuthCenterMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class AuthCenterDbContext : AbpDbContext<AuthCenterDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside AuthCenterDbContextModelCreatingExtensions.ConfigureAuthCenter
         */
        public DbSet<SharedIdentityResource> SharedIdentityResources { get; set; }
        public DbSet<SharedApiScopes> ApiScopes { get; set; }

        public AuthCenterDbContext(DbContextOptions<AuthCenterDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the AuthCenterEfCoreEntityExtensionMappings class
                 */
            });

            builder.Entity<SharedIdentityResource>(b =>
            {
                b.ToTable(AbpIdentityServerDbProperties.DbTablePrefix + "IdentityResources");

                b.ConfigureByConvention();

                b.Property(x => x.Name).HasMaxLength(ApiResourceConsts.NameMaxLength).IsRequired();
            });

            builder.Entity<SharedApiScopes>(b =>
            {
                b.ToTable(AbpIdentityServerDbProperties.DbTablePrefix + "ApiScopes");

                b.ConfigureByConvention();

                b.HasKey(x => new { x.ApiResourceId, x.Name });
                b.Property(x => x.Name).HasMaxLength(ApiResourceConsts.NameMaxLength).IsRequired();
            });

            /* Configure your own tables/entities inside the ConfigureAuthCenter method */

            builder.ConfigureAuthCenter();
        }
    }
}
