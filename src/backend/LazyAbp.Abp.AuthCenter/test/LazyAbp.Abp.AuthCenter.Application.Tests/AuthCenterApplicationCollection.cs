using LazyAbp.Abp.AuthCenter.EntityFrameworkCore;
using Xunit;

namespace LazyAbp.Abp.AuthCenter
{
    [CollectionDefinition(AuthCenterTestConsts.CollectionDefinitionName)]
    public class AuthCenterApplicationCollection : AuthCenterEntityFrameworkCoreCollectionFixtureBase
    {

    }
}
