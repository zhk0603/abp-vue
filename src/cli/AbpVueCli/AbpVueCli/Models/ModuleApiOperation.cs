using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class ModuleApiOperation
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public OpenApiOperation Operation { get; set; }

        public ModuleApiOperation()
        {
        }
    }
}