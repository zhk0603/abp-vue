using Abp.VueTemplate.MenuManagement;
using AutoMapper;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuManagementApplicationAutoMapperProfile : Profile
    {
        public MenuManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Menu, MenuDto>();
            CreateMap<CreateOrUpdateMenuDto, Menu>();
        }
    }
}
