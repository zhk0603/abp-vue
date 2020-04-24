using System;

namespace Abp.VueTemplate.MenuManagement
{
    [Serializable]
    public class MenuGrantCacheItem
    {
        public Guid MenuId { get; set; }
        public bool IsGranted { get; set; }

        public MenuGrantCacheItem()
        {

        }

        public MenuGrantCacheItem(Guid menuId, bool isGranted)
        {
            MenuId = menuId;
            IsGranted = isGranted;
        }

        public static string CalculateCacheKey(Guid menuId, string providerName, string providerKey)
        {
            return "pn:" + providerName + ",pk:" + providerKey + ",m:" + menuId;
        }
    }
}