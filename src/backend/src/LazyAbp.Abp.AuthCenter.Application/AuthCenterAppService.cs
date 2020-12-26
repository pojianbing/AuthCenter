using System;
using System.Collections.Generic;
using System.Text;
using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.Application.Services;

namespace LazyAbp.Abp.AuthCenter
{
    /* Inherit your application services from this class.
     */
    public abstract class AuthCenterAppService : ApplicationService
    {
        protected AuthCenterAppService()
        {
            LocalizationResource = typeof(AuthCenterResource);
        }
    }
}
