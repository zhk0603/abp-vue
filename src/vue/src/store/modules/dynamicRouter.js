import { constantRoutes } from '@/router'
import store from '../index'
import Layout from '@/layout'

const _import = require('../../router/_import_' + process.env.NODE_ENV)

const state = {
  addRouteres: []
}

const mutations = {
  setAddRouteres: (state, routers) => {
    state.addRouteres = routers
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
      const routers = getRemoteRouteres()
      routers.push({
        path: '*',
        redirect: '/404',
        hidden: true
      })
      commit('setAddRouteres', routers)
      // }

      resolve(store.getters.addRouters)
    })
  }
}

function getRemoteRouteres() {
  // 这里调用api 获取路由表，假如后台返回的内容如下：
  var remoteRouters = [
    // {
    //   path: '/example',
    //   component: 'Layout',
    //   redirect: '/example/table',
    //   name: 'Example',
    //   meta: { title: 'Example （Dynamic Router）', icon: 'example' },
    //   children: [
    //     {
    //       path: 'table',
    //       name: 'Table',
    //       component: 'table/index',
    //       meta: { title: 'Table', icon: 'table' }
    //     },
    //     {
    //       path: 'tree',
    //       name: 'Tree',
    //       component: 'tree/index',
    //       meta: { title: 'Tree', icon: 'tree' }
    //     }
    //   ]
    // }
  ]

  var routers = buildRouteres(remoteRouters)

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
