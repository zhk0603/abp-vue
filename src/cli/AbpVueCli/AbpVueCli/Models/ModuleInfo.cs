using System;
using System.Collections.Generic;
using AbpVueCli.Commands;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Models
{
    public class ModuleInfo
    {
        public string Name => Option.Module;
        public string CamelCaseName => Option.Module.ToCamelCase();
        public string PascalCaseName => Option.Module.ToPascalCase();
        public GenerateCommandOptionBasic Option { get; set; }
        public ProjectInfo ProjectInfo { get; set; }
        public List<ModuleApiOperation> ModuleApis { get; set; } = new List<ModuleApiOperation>();

        public ModuleInfo(IEnumerable<KeyValuePair<string, OpenApiPathItem>> paths)
        {
            foreach(var path in paths)
            {
                foreach (var o in path.Value.Operations)
                {
                    ModuleApis.Add(new ModuleApiOperation
                    {
                        Url = path.Key,
                        Method = o.Key.ToString().ToLower(),
                        Operation = o.Value
                    });
                }
            }
        }
    }
}