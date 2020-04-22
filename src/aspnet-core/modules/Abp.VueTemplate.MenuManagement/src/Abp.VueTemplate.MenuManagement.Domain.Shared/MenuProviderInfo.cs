using JetBrains.Annotations;
using Volo.Abp;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuProviderInfo
    {
        public string Name { get; }

        public string Key { get; }

        public MenuProviderInfo([NotNull]string name, [NotNull]string key)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(key, nameof(key));

            Name = name;
            Key = key;
        }
    }
}