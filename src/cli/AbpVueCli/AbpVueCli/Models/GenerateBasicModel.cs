using System;
using System.Text;

namespace AbpVueCli.Models
{
    public class BasicGenerateModel
    {
        public string Name { get; set; }
        public string CamelCaseName => Name.ToCamelCase();
        public string PascalCaseName => Name.ToPascalCase();

        public ModuleInfo ModuleInfo { get; set; }
    }
}
