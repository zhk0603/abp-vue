import router from './router'
import store from './store'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import getPageTitle from '@/utils/get-page-title'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

router.beforeEach(async(to, from, next) => {
  // start progress bar
  NProgress.start()
  console.log('store', store)
  // set page title
  document.title = getPageTitle(to.meta.title)
  store.dispatch('oidc/oidcCheckAccess', to).then(hasAccess => {
    console.log('hasAccess:', hasAccess)
    if (hasAccess) {
      if (
        store.getters.addRouters.length === 0 &&
        store.state.oidc.access_token
      ) {
        store.dispatch('permission/generateRoutes').then(routers => {
          console.log('addRouteres:', routers)
          router.addRoutes(routers)
          next({
            ...to,
            replace: true
          })
        })
      } else {
        next()
      }
    }
  })
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
