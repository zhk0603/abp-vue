import { vuexOidcCreateStoreModule } from 'vuex-oidc'
import setting from '@/settings.js'
import store from '@/store'
import router from '@/router'

const oidcStoreSetting = {
  namespaced: true,
  dispatchEventsOnWindow: false,
  publicRoutePaths: setting.publicRoutePaths
}

const oidcEventListeners = {
  userLoaded: user => {
    console.log('OIDC user is loaded:', user)
    const curRouter = router.currentRoute
    console.log(curRouter)
    if (store.getters.addRouters.length === 0) {
      console.log(111)
      // router.go(0)
      // router.replace({
      //   path: curRouter.path
      // })

      // location.reload()
      // store.dispatch('permission/generateRoutes').then(routers => {
      //   console.log('addRouteres:', routers)
      //   router.addRoutes(routers)
      //   router.replace({
      //     path: curRouter.path
      //   })
      // })
    }
  },
  userUnloaded: () => console.log('OIDC user is unloaded'),
  accessTokenExpiring: () => console.log('Access token will expire'),
  accessTokenExpired: () => console.log('Access token did expire'),
  silentRenewError: () => console.log('OIDC user is unloaded'),
  userSignedOut: () => {
    console.log('OIDC user is signed out')
    // 清理token
    store.dispatch('oidc/removeUser')
    store.dispatch('app/toggleUnauthorizedDialogVisible', true)
  },
  oidcError: payload => console.log('OIDC error', payload)
}

export default vuexOidcCreateStoreModule(
  setting.oidcSettings,
  oidcStoreSetting,
  oidcEventListeners
)
