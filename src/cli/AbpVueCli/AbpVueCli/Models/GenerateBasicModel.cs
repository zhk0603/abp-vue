using System;
using System.Text;

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
}
