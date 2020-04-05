import request from '@/utils/request'

const permissionApi = {}

/**
 * 获取权限
 */
permissionApi.get = params => {
  return request.get('/api/abp/permissions', {
    params
  })
}

/**
 * 更新
 */
permissionApi.put = (params, data) => {
  return request({
    url: '/api/abp/permissions',
    method: 'PUT',
    params: params,
    data: data
  })
}

export default permissionApi
