using System.Security.Claims;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantRuntimeCheckerContent
    {
        public Menu Menu { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}