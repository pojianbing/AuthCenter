using Xunit;

namespace LazyAbp.Abp.AuthCenter.EntityFrameworkCore
{
    [CollectionDefinition(AuthCenterTestConsts.CollectionDefinitionName)]
    public class AuthCenterEntityFrameworkCoreCollection : ICollectionFixture<AuthCenterEntityFrameworkCoreFixture>
    {

    }
}
