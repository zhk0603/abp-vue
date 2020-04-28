using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateModelModel : BasicGenerateModel, IGenerateModelModel
    {
        public OpenApiSchema RequestBodySchema { get; set; }
    }

    public interface IGenerateModelModel
    {
        OpenApiSchema RequestBodySchema { get; set; }
    }
}