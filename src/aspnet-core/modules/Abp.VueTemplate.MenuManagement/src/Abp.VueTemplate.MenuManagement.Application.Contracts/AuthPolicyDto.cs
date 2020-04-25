using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.VueTemplate.MenuManagement
{
    public class AuthPolicyDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<AuthPolicyDto> Children { get; set; } = new List<AuthPolicyDto>();
    }
}
