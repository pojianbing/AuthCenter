using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace LazyAbp.Abp.AuthCenter.Web.Pages
{
    public class IndexModel : AuthCenterPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}