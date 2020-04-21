using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuRequestDto
    {
        public string Name { get; set; }

        public MenuEnumType? Type { get; set; }
    }
}
