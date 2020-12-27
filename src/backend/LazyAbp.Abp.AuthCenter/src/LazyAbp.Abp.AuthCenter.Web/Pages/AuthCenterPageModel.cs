using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LazyAbp.Abp.AuthCenter.Web.Pages
{
    public abstract class AuthCenterPageModel : AbpPageModel
    {
        protected AuthCenterPageModel()
        {
            LocalizationResourceType = typeof(AuthCenterResource);
        }
    }
}