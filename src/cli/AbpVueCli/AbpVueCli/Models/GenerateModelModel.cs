using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateModelModel : BasicGenerateModel
    {
        public OpenApiSchema RequestBodySchema { get; set; }
    }
}