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
            //CreateMap<MenuDto, MenuInfo>()
            //    .ForMember(d => d.Component, opt => { opt.MapFrom(s => s.ComponentPath); })
            //    .ForMember(d => d.Path, opt => { opt.MapFrom(s => s.RouterPath); })
            //    .ForMember(d => d.Meta,
            //        opt =>
            //        {
            //            opt.MapFrom(s => new MenuMetaInfo
            //            {
            //                Icon = s.Icon,
            //                Title = s.DisplayName
            //            });
            //        });

            CreateMap<Menu, VueMenu>()
                .ForMember(d => d.Component, opt => { opt.MapFrom(s => s.ComponentPath); })
                .ForMember(d => d.Path, opt => { opt.MapFrom(s => s.RouterPath); })
                .ForMember(d => d.Meta,
                    opt =>
                    {
                        opt.MapFrom(s => new MenuMeta
                        {
                            Icon = s.Icon,
                            Title = s.DisplayName
                        });
                    });
        }
    }
}