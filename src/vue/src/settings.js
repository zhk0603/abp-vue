const idpBase = process.env.VUE_APP_IDP_BASE
const vueBase = process.env.VUE_APP_VUE_BASE

module.exports = {
  title: 'abp vue',

  /**
   * @type {boolean} true | false
   * @description Whether fix the header
   */
  fixedHeader: false,

  /**
   * @type {boolean} true | false
   * @description Whether show the logo in sidebar
   */
  sidebarLogo: false,

  /**
   * oidc 配置。
   */
  oidcSettings: {
    authority: `${idpBase}`,
    client_id: 'VueTemplate_Vue',
    redirect_uri: `${vueBase}/signin-oidc`,
    post_logout_redirect_uri: `${vueBase}/`,
    silent_redirect_uri: `${vueBase}/silent-renew-oidc`,
    popup_redirect_uri: `${vueBase}/signin-oidc-popup`,
    scope: 'VueTemplate role openid profile address email phone',
    response_type: 'id_token token',
    automaticSilentRenew: true,
    automaticSilentSignin: true // 自动无声登录
  },

  /**
   * 公开路径，不需要权限
   */
  publicRoutePaths: ['/404', '/login', '/dashboard']
}
