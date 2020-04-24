using JetBrains.Annotations;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantInfo
    {
        public static MenuGrantInfo NonGranted { get; } = new MenuGrantInfo(false);

        public virtual bool IsGranted { get; }

        public virtual string ProviderKey { get; }

        public MenuGrantInfo(bool isGranted, [CanBeNull] string providerKey = null)
        {
            IsGranted = isGranted;
            ProviderKey = providerKey;
        }
    }
}