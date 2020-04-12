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
    public class GenerateListViewStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src", "views");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");
            var targetDirectory = Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "views");

            ModuleApiOperation getListApi = context.GetVariable<ModuleApiOperation>("GetListModuleApi");

            var model = new GenerateListModel
            {
                Name = modelInfo.Name,
                ApiOperation = getListApi,
                ModuleInfo = modelInfo,
                ProjectInfo = modelInfo.ProjectInfo,
                GenerateCreate = true,
                GenerateEdit = true,
                QueryParams = TemplateHelper.GetQueryParameters(modelInfo.ProjectInfo, getListApi.Operation),
                ListProperty = GetListProperty(getListApi, modelInfo.ProjectInfo)
            };

            await GenerateFile(tempDir, targetDirectory, model, true);

            return Done();
        }

        private IDictionary<string, OpenApiSchema> GetListProperty(ModuleApiOperation listApi, ProjectInfo projectInfo)
        {
            if ((listApi.Operation.Responses.TryGetValue("200", out var statusCode200Response) &&
                 statusCode200Response.Content.TryGetValue("application/json", out var openApiMediaType)))
            {
                if (!projectInfo.ListPropertySchemaPath.IsNullOrWhiteSpace())
                {
                    var paths = projectInfo.ListPropertySchemaPath.Split(new [] {'/'},
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

        private async Task GenerateFile(string groupDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(groupDirectory, "*index.vue.sbntxt", SearchOption.AllDirectories))
            {
                Logger.LogDebug("Generating using template file: {file}", file);
                var targetFilePathNameTemplate = file.Replace(groupDirectory, targetDirectory);
                var targetFilePathName = TextGenerator.GenerateByTemplateText(targetFilePathNameTemplate, model).RemovePostFix(".sbntxt");
                if (File.Exists(targetFilePathName) && !overwrite)
                {
                    Logger.LogInformation("File “{targetFilePathName}” already exists, skip generating.",
                        targetFilePathName);
                    continue;
                }

                var templateText = await File.ReadAllTextAsync(file);
                var contents = TextGenerator.GenerateByTemplateText(templateText, model);

                var dir = Path.GetDirectoryName(targetFilePathName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(targetFilePathName, contents);
                Logger.LogInformation("File “{targetFilePathName}” successfully generated.", targetFilePathName);
            }
        }
    }

    public class GenerateCreateViewStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src", "views");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");
            var targetDirectory = Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "views");

            ModuleApiOperation postApi = context.GetVariable<ModuleApiOperation>("PostModuleApi");
            var apiSchema = postApi.Operation.RequestBody.Content.First().Value.Schema;

            var model = new
            {
                Name = modelInfo.Name,
                CamelCaseName = modelInfo.CamelCaseName,
                PascalCaseName = modelInfo.PascalCaseName,
                Url = modelInfo.Option.ModulePrefix,
                ApiSchema = apiSchema,
                GenerateCreate = true,
                GenerateEdit = true
            };

            await GenerateFile(tempDir, targetDirectory, model, true);

            return Done();
        }

        private async Task GenerateFile(string groupDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(groupDirectory, "*CreateOrEditForm.vue.sbntxt OR *CreateDialog.vue.sbntxt", SearchOption.AllDirectories))
            {
                Logger.LogDebug("Generating using template file: {file}", file);
                var targetFilePathNameTemplate = file.Replace(groupDirectory, targetDirectory);
                var targetFilePathName = TextGenerator.GenerateByTemplateText(targetFilePathNameTemplate, model).RemovePostFix(".sbntxt");
                if (File.Exists(targetFilePathName) && !overwrite)
                {
                    Logger.LogInformation("File “{targetFilePathName}” already exists, skip generating.",
                        targetFilePathName);
                    continue;
                }

                var templateText = await File.ReadAllTextAsync(file);
                var contents = TextGenerator.GenerateByTemplateText(templateText, model);

                var dir = Path.GetDirectoryName(targetFilePathName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(targetFilePathName, contents);
                Logger.LogInformation("File “{targetFilePathName}” successfully generated.", targetFilePathName);
            }
        }
    }
}