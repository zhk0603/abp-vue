using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateCreateModel : BasicGenerateModel
    {
        public IDictionary<string, OpenApiSchema> Properties { get; set; }
    }
}