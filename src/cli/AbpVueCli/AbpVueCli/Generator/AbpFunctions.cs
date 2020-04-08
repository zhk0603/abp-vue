using System;
using System.Linq;
using Scriban.Runtime;


namespace AbpVueCli.Generator
{
    public class AbpFunctions : ScriptObject
    {
        public static string CamelCase(string text)
        {
            var parts = text.Split('.')
                .Select(part => part.ToCamelCase());
            return string.Join('.', parts);
        }

        public static bool IsIgnoreProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Type == "Guid?" && propertyInfo.Name == "TenantId") return true;
            return false;
        }
    }

    public class PropertyInfo
    {
        public PropertyInfo(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}
