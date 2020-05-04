using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.VirtualFileSystem;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate
{

    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(Volo.Abp.AspNetCore.Mvc.UI.Bundling.TagHelpers.AbpTagHelperScriptService))]
    public class MyTagHelperScriptService : Volo.Abp.AspNetCore.Mvc.UI.Bundling.TagHelpers.AbpTagHelperScriptService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyTagHelperScriptService(
            IBundleManager bundleManager,
            IWebContentFileProvider webContentFileProvider,
            IOptions<AbpBundlingOptions> options,
            IWebHostEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor
        ) : base(bundleManager, webContentFileProvider, options, hostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void AddHtmlTag(TagHelperContext context, TagHelperOutput output, string file)
        {
            if (file.StartsWith("/"))
            {
                file = _httpContextAccessor.HttpContext.Request.PathBase + file;
            }
            base.AddHtmlTag(context, output, file);
        }
    }

    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(Volo.Abp.AspNetCore.Mvc.UI.Bundling.TagHelpers.AbpTagHelperStyleService))]
    public class MyTagHelperStyleService : Volo.Abp.AspNetCore.Mvc.UI.Bundling.TagHelpers.AbpTagHelperStyleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyTagHelperStyleService(
            IBundleManager bundleManager,
            IWebContentFileProvider webContentFileProvider,
            IOptions<AbpBundlingOptions> options,
            IWebHostEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor
        ) : base(bundleManager, webContentFileProvider, options, hostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void AddHtmlTag(TagHelperContext context, TagHelperOutput output, string file)
        {
            if (file.StartsWith("/"))
            {
                file = _httpContextAccessor.HttpContext.Request.PathBase + file;
            }
            base.AddHtmlTag(context, output, file);
        }
    }
}
