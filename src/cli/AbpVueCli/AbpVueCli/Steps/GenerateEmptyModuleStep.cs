using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Generator;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Steps
{
    public class GenerateEmptyModuleStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            Logger.LogInformation("生成一个空的模块。");
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"模板目录 {tempDir} 不存在。");

            string targetDirectory = Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src");
            var overwrite = context.GetVariable<bool>("Overwrite");

            var model = new GenerateEmptyModel
            {
                Name = modelInfo.Name,
                ModuleInfo = modelInfo,
                GenerateCreate = true,
                GenerateEdit = true
            };

            await GenerateFiles(tempDir, targetDirectory, model, overwrite);

            return Done();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*.sbntxt", SearchOption.AllDirectories))
            {
                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, overwrite);
            }
        }
    }

    public class GenerateEmptyModel : BasicGenerateModel, IGenerateCreateModel, IGenerateListModel, IGenerateModelModel
    {
        public IDictionary<string, OpenApiSchema> Properties { get; set; } = new Dictionary<string, OpenApiSchema>();
        public bool GenerateCreate { get; set; }
        public bool GenerateEdit { get; set; }
        public IEnumerable<OpenApiParameterWrap> QueryParams { get; set; } = new List<OpenApiParameterWrap>();
        public IDictionary<string, OpenApiSchema> ListProperty { get; set; } = new Dictionary<string, OpenApiSchema>();
        public OpenApiSchema RequestBodySchema { get; set; }
    }
}