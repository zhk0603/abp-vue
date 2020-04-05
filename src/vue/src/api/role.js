import request from '@/utils/request'

const roleApi = {}

/**
 * 获取所有角色
 */
roleApi.getAll = () => {
  return request.get('/api/identity/roles/all')
}

export default roleApi
