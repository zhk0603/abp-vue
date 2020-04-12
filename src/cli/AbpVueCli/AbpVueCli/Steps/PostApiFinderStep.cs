using System;
using System.Linq;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace AbpVueCli.Steps
{
    public class PostApiFinderStep : Step
    {
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");
            ModuleApiOperation postApi = modelInfo.ModuleApis.FirstOrDefault(api =>
                api.Url.Equals(modelInfo.Option.ModulePrefix, StringComparison.OrdinalIgnoreCase) &&
                api.Method.Equals("post", StringComparison.OrdinalIgnoreCase));

            if (postApi == null)
            {
                Logger.LogError("找不到 POST API");
                return Fault("找不到 POST API");
            }

            var apiSchema = postApi.Operation.RequestBody.Content.First().Value.Schema;
            Logger.LogInformation("POST API:{api}, Model:{model}", modelInfo.Option.ModulePrefix,
                apiSchema.Reference.Id);

            context.SetVariable("PostModuleApi", postApi);

            return Done();
        }
    }

    public class GetListApiFinderStep : Step
    {
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");
            ModuleApiOperation api = modelInfo.ModuleApis.FirstOrDefault(api =>
                api.Url.Equals(modelInfo.Option.ModulePrefix, StringComparison.OrdinalIgnoreCase) &&
                api.Method.Equals("get", StringComparison.OrdinalIgnoreCase));

            if (api == null)
            {
                Logger.LogError("找不到 GET LIST API");
                return Fault("找不到 GET LIST API");
            }

            Logger.LogInformation("GET LIST API:{api}", modelInfo.Option.ModulePrefix);

            context.SetVariable("GetListModuleApi", api);

            return Done();
        }
    }
}