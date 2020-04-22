using System;

namespace Abp.VueTemplate.MenuManagement
{
    public class UpdateMenuGrantDto
    {
        public Guid Id { get; set; }
        public string PermissionKey { get; set; }
        public bool IsGranted { get; set; }
    }
}