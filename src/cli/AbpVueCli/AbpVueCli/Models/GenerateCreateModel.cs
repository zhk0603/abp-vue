using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateCreateModel : BasicGenerateModel, IGenerateCreateModel
    {
        public IDictionary<string, OpenApiSchema> Properties { get; set; }
    }

    public interface IGenerateCreateModel
    {
        IDictionary<string, OpenApiSchema> Properties { get; set; }
    }
}