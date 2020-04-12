using System;
using System.Collections.Generic;
using System.Text;
using AbpVueCli.Generator;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public abstract class BasicGenerateModel
    {
        public string Name { get; set; }
        public string CamelCaseName => Name.ToCamelCase();
        public string PascalCaseName => Name.ToPascalCase();

        public ProjectInfo ProjectInfo { get; set; }
        public ModuleInfo ModuleInfo { get; set; }
    }

    public class GenerateListModel : BasicGenerateModel
    {
        public bool GenerateCreate { get; set; }
        public bool GenerateEdit { get; set; }
        public ModuleApiOperation ApiOperation { get; set; }
        public IEnumerable<OpenApiParameterWrap> QueryParams { get; set; }
        public IDictionary<string,OpenApiSchema> ListProperty { get; set; }
    }
}
