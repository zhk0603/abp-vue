using System.Collections.Generic;
using AbpVueCli.Generator;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class GenerateListModel : BasicGenerateModel, IGenerateListModel
    {
        public bool GenerateCreate { get; set; }
        public bool GenerateEdit { get; set; }
        public IEnumerable<OpenApiParameterWrap> QueryParams { get; set; }
        public IDictionary<string,OpenApiSchema> ListProperty { get; set; }
    }

    public interface IGenerateListModel
    {
        bool GenerateCreate { get; set; }
        bool GenerateEdit { get; set; }
        IEnumerable<OpenApiParameterWrap> QueryParams { get; set; }
        IDictionary<string, OpenApiSchema> ListProperty { get; set; }
    }
}