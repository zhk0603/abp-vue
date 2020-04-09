using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AbpVueCli.Commands;
using AbpVueCli.Module;
using AbpVueCli.Steps;
using Microsoft.OpenApi.Models;
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
                        .Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (path.Count > 0 && !(path[0].StartsWith("get", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("delete", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("post", StringComparison.OrdinalIgnoreCase) ||
                                        path[0].StartsWith("put", StringComparison.OrdinalIgnoreCase) ))
                {
                    path.Insert(0, api.Method.ToLower());
                }

                funcName = $"{string.Join("", path.Where(x => !x.StartsWith("{")))}".ToCamelCase()
                        .Replace("-", "")
                        .Replace(".", "");

                @params = path.Where(x => x.StartsWith("{"))
                    .Select(x =>
                        x.Replace("{", "")
                            .Replace("}", "")
                            .ToCamelCase())
                    .ToList();
            }

            TryAddParams(@params, api.Operation);

            return $"{funcName} = ({string.Join(",", @params)})";
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
                @params = ",\r\n    body";
            }

            return @params;
        }

        private static void TryAddParams(List<string> @params, OpenApiOperation operation)
        {
            var queryParams = operation.Parameters?.Where(x => x.In == ParameterLocation.Query);
            if (queryParams != null && queryParams.Any())
            {
                @params.AddIfNotContains("params");
            }

            if (operation.RequestBody != null)
            {
                @params.AddIfNotContains("body");
            }
        }
    }
}
