import Vue from 'vue'
import Vuex from 'vuex'
import getters from './getters'
import app from './modules/app'
import settings from './modules/settings'
import user from './modules/user'
import oidc from './modules/oidc'
import dynamicRouter from './modules/dynamicRouter'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    app,
    settings,
    user,
    oidc,
    dynamicRouter
  },
  getters
})

export default store
