using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using AbpVueCli.Commands;
using AbpVueCli.Extensions;
using AbpVueCli.Models;
using AbpVueCli.Steps;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scriban.Runtime;

namespace AbpVueCli.Generator
{
    public class TemplateHelper : ScriptObject
    {
        public static string GenerateApiFuncName(GenerateCommandOptionBasic options, ModuleApiOperation api)
        {
            /*
             * 通过约定生成api方法名。
             * 获取列表   get      {path}
             * 获取详情   get      {path}/{id}
             * 新增       post     {path}
             * 编辑:      put      {path}/{id}
             * 删除:      delete   {path}/{id}
             */

            string funcName = null;
            List<string> @params = new List<string>();
            if (api.Url.Equals(options.ModulePrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (api.Method.Equals("get", StringComparison.OrdinalIgnoreCase))
                {
                    funcName = "getList";
                    @params.Add("params");
                }
                else if (api.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
                {
                    funcName = "post";
                    @params.Add("body");
                }
            }
            else if (api.Url.Equals(options.ModulePrefix + "/{id}", StringComparison.OrdinalIgnoreCase))
            {
                if (api.Method.Equals("get", StringComparison.OrdinalIgnoreCase))
                {
                    funcName = "get";
                    @params.Add("id");
                }
                else if (api.Method.Equals("put", StringComparison.OrdinalIgnoreCase))
                {
                    funcName = "put";
                    @params.Add("id");
                    @params.Add("body");

                }
                else if (api.Method.Equals("delete", StringComparison.OrdinalIgnoreCase))
                {
                    funcName = "delete";
                    @params.Add("id");
                }
            }
            else
            {
                var path = api.Url.Substring(options.ModulePrefix.Length)
                    .Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(x => x.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries))
                    .SelectMany(x => x.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => x.ToPascalCase())
                    .ToList();

                if (path.Count > 0 && !(path[0].StartsWith("get", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("delete", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("post", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("put", StringComparison.OrdinalIgnoreCase)))
                {
                    path.Insert(0, api.Method.ToLower());
                }

                funcName = $"{string.Join("", path.Where(x => !x.StartsWith("{")))}"
                    .ToCamelCase();

                @params = path.Where(x => x.StartsWith("{"))
                    .Select(x =>
                        x.Replace("{", "")
                            .Replace("}", "")
                            .ToCamelCase())
                    .ToList();
            }

            TryAddParams(@params, api.Operation);

            return $"{funcName} = ({string.Join(", ", @params)})";
        }

        public static string GenerateApiUrl(string url)
        {
            return url.Replace("{", "${");
        }

        public static string GenerateApiParams(ModuleApiOperation api)
        {
            var @params = "";
            if (api.Operation.Parameters.Any(x => x.In == ParameterLocation.Query))
            {
                @params = ",\r\n    params";
            }

            if (api.Operation.RequestBody != null)
            {
                @params = ",\r\n    data: body";
            }

            return @params;
        }

        public static string GenerateViewModel(OpenApiSchema apiSchema)
        {
            return apiSchema.ToJson();
        }

        public static string GenerateRules(OpenApiSchema apiSchema)
        {
            var rulesDic = new Dictionary<string, List<JObject>>();

            foreach (var propertyItem in apiSchema.Properties)
            {
                var rules = new List<JObject>();

                if (apiSchema.Required.Contains(propertyItem.Key))
                {
                    var requiredRule = new JObject();
                    requiredRule["required"] = true;
                    requiredRule["message"] = $"请输入{propertyItem.Value.Description ?? propertyItem.Key}";
                    requiredRule["trigger"] = "blur";
                    rules.Add(requiredRule);
                }

                if (propertyItem.Value.MinLength.HasValue)
                {
                    var lenRule = new JObject();
                    lenRule["min"] = propertyItem.Value.MinLength;
                    lenRule["max"] = propertyItem.Value.MaxLength;
                    lenRule["message"] = $"长度在 {propertyItem.Value.MinLength} 到 {propertyItem.Value.MaxLength} 个字符";
                    lenRule["trigger"] = "blur";
                    rules.Add(lenRule);
                }

                if (!propertyItem.Value.Type.IsNullOrWhiteSpace())
                {
                    var formatRule = new JObject();
                    formatRule["type"] = GetJsFormatType(propertyItem.Value);
                    formatRule["message"] =
                        $"{propertyItem.Value.Description ?? propertyItem.Key} 必须为 {formatRule["type"]}";
                    formatRule["trigger"] = "change";
                    rules.Add(formatRule);
                }

                if (rules.Count > 0)
                {
                    rulesDic.Add(propertyItem.Key, rules);
                }
            }

            return JsonConvert.SerializeObject(rulesDic, Formatting.Indented);
        }

        private static string GetJsFormatType(OpenApiSchema propertySchema)
        {
            string type = propertySchema.Type;

            if (propertySchema.Format == "date-time")
            {
                type = "date";
            }

            return type;
        }

        public static IEnumerable<OpenApiParameterWrap> GetQueryParameters(ProjectInfo projectInfo,
            OpenApiOperation apiOperation)
        {
            return apiOperation.Parameters
                .Select(x => new OpenApiParameterWrap(x))
                .Where(x =>
                    x.Parameter.In == ParameterLocation.Query &&
                    projectInfo.ListQueryIgnoreParams.All(y => y.ToLower() != x.Name.ToLower()));
        }

        private static void TryAddParams(List<string> @params, OpenApiOperation operation)
        {
            var haveQueryParams = operation.Parameters?.Any(x => x.In == ParameterLocation.Query);
            if (haveQueryParams == true)
            {
                @params.AddIfNotContains("params");
            }

            if (operation.RequestBody != null)
            {
                @params.AddIfNotContains("body");
            }
        }
    }

    public class OpenApiParameterWrap
    {
        public OpenApiParameterWrap(OpenApiParameter parameter)
        {
            Parameter = parameter;
        }

        public OpenApiParameter Parameter { get; }

        public string Name => Parameter.Name.Split('.').Last();

        public string CamelCaseName => Name.ToCamelCase();
        public string PascalCaseName => Name.ToPascalCase();

        public string Description => Parameter.Description ?? PascalCaseName;
    }
}
