using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LazyAbp.Abp.AuthCenter.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AuthCenterController : AbpController
    {
        protected AuthCenterController()
        {
            LocalizationResource = typeof(AuthCenterResource);
        }
    }
}