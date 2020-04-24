import permission from './permission'
import Vue from 'vue'

const install = function() {
  Vue.directive('permission', permission)
}

permission.install = install
export default permission
