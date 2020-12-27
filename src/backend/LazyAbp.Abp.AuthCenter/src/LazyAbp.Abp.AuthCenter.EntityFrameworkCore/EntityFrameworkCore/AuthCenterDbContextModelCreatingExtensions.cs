using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    public static class AuthCenterDbContextModelCreatingExtensions
    {
        public static void ConfigureAuthCenter(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(AuthCenterConsts.DbTablePrefix + "YourEntities", AuthCenterConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}