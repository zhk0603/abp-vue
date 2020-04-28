using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Abp.VueTemplate.Web.Pages
{
    public class IndexModel : VueTemplatePageModel
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