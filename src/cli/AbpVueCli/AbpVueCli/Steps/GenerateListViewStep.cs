using System;
using System.Collections.Generic;
using System.IO;
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
    public class GenerateListViewStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src", "views");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"模板目录 {tempDir} 不存在。");

            string targetDirectory = modelInfo.Option.OutputFolder.IsNullOrWhiteSpace()
                ? Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "views")
                : Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, modelInfo.Option.OutputFolder));

            ModuleApiOperation getListApi = context.GetVariable<ModuleApiOperation>("GetListModuleApi");

            var model = new GenerateListModel
            {
                Name = modelInfo.Name,
                ModuleInfo = modelInfo,
                ProjectInfo = modelInfo.ProjectInfo,

                ApiOperation = getListApi,
                GenerateCreate = true,
                GenerateEdit = true,
                QueryParams = TemplateHelper.GetQueryParameters(modelInfo.ProjectInfo, getListApi.Operation),
                ListProperty = GetListProperty(getListApi, modelInfo.ProjectInfo)
            };

            var overwrite = context.GetVariable<bool>("Overwrite");
            await GenerateFiles(tempDir, targetDirectory, model, overwrite);

            return Done();
        }

        private IDictionary<string, OpenApiSchema> GetListProperty(ModuleApiOperation listApi, ProjectInfo projectInfo)
        {
            if ((listApi.Operation.Responses.TryGetValue("200", out var statusCode200Response) &&
                 statusCode200Response.Content.TryGetValue("application/json", out var openApiMediaType)))
            {
                if (!projectInfo.ListPropertySchemaPath.IsNullOrWhiteSpace())
                {
                    var paths = projectInfo.ListPropertySchemaPath.Split(new [] {'.'},
                        StringSplitOptions.RemoveEmptyEntries);

                    OpenApiSchema propertySchema = openApiMediaType.Schema;
                    foreach (var path in paths)
                    {
                        if (!propertySchema.Properties.TryGetValue(path, out propertySchema))
                        {
                            Logger.LogWarning("路径不匹配，{path}", projectInfo.ListPropertySchemaPath);
                            goto defVal;
                        }
                    }

                    if ("array".Equals(propertySchema.Type))
                    {
                        return propertySchema.Items.Properties;
                    }

                    return propertySchema.Properties;
                }

                defVal:
                return openApiMediaType.Schema.Properties;
            }

            Logger.LogError(
                "请确认接口的 Responses 中存在 HttpStatusCode 等于 200 的响应，并且该响应的 Media type 要包含 application/json，URL:{url}",
                listApi.Url);

            return new Dictionary<string, OpenApiSchema>();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*index.vue.sbntxt", SearchOption.AllDirectories))
            {
                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, overwrite);
            }
        }
    }
}