import { constantRoutes } from '@/router'
import store from '../index'
import Layout from '@/layout'
import menuGrantApi from '@/api/menuGrant'

const _import = require('../../router/_import_' + process.env.NODE_ENV)

const state = {
  addRouteres: [],
  permissions: []
}

const mutations = {
  setAddRouteres: (state, routers) => {
    state.addRouteres = routers
  },
  setPermissions: (state, permissions) => {
    state.permissions = permissions
  }
}

const getters = {
  allRouteres: state => {
    return constantRoutes.concat(state.addRouteres)
  }
}

const actions = {
  generateRoutes({ commit }, param) {
    return new Promise(resolve => {
      console.log('generateRoutes……')
      // if (store.state.oidc.access_token && store.getters.addRouters.length === 0) {
      getRemoteRouteres(commit).then(routers => {
        routers.push({
          path: '*',
          redirect: '/404',
          hidden: true
        })
        commit('setAddRouteres', routers)

        resolve(store.getters.addRouters)
      })
    })
  }
}

async function getRemoteRouteres(commit) {
  var res = await menuGrantApi.getList()
  commit('setPermissions', res.permissionGrants)
  var routers = buildRouteres(res.menus)
  return routers
}

function buildRouteres(routers) {
  routers.forEach(item => {
    if (item.component === 'Layout') {
      item.component = Layout
    } else {
      item.component = _import(item.component)
    }
    if (item.children && item.children.length > 0) {
      buildRouteres(item.children)
    } else {
      // eslint-disable-next-line no-unused-vars
      const { redirect, ...results } = item
      item = results
    }
  })
  return routers
}

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}
