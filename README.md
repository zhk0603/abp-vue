# abp-vue
abp account、 identity、tenant 模块前端部分的vue实现

### [✨后端api](https://github.com/zhk0603/abp-vue/tree/master/src/aspnet-core)
  此项目是abp-vue的后端接口项目，使用abp cli创建的，所以默认集成了官方id4、 identity、 tenant-management 等模块。
### [✨菜单模块](https://github.com/zhk0603/abp-vue/tree/master/src/aspnet-core/modules/Abp.VueTemplate.MenuManagement)
abp-vue是前后端分离的，前端采用了Vue，通过菜单模块实现了动态路由、页面权限控制。
  
### [✨前端Vue](https://github.com/zhk0603/abp-vue/tree/master/src/vue)
  abp 几个基本模块/以及上面的菜单模块 前端部分的vue实现。

### [✨cli工具](https://github.com/zhk0603/abp-vue/tree/master/src/cli/AbpVueCli)
为了快速开发vue程序，所以开发了一个通过读取OpenApi文档，并自动生成了一些基础项目代码，以减轻工作量。  
此项目基本思路及源码来源 [abphelper.cli](https://github.com/EasyAbp/AbpHelper.CLI)，在此表示感谢 🎉🎉。
