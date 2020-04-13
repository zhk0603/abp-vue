using System.Collections.Generic;
using AbpVueCli.Generator;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateListModel : BasicGenerateModel
    {
        public bool GenerateCreate { get; set; }
        public bool GenerateEdit { get; set; }
        public ModuleApiOperation ApiOperation { get; set; }
        public IEnumerable<OpenApiParameterWrap> QueryParams { get; set; }
        public IDictionary<string,OpenApiSchema> ListProperty { get; set; }
    }
}