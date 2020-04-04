import request from '@/utils/request'

const userApi = {}

/**
 * 获取列表
 */
userApi.list = (params) => {
  return request.get('/api/identity/users', {
    params
  })
}

/**
 * 新增
 */
userApi.post = (data) => {
  return request.post('/api/identity/users', data)
}

/**
 * 更新
 */
userApi.put = (data) => {
  return request.put('/api/identity/users', data)
}

userApi.getInfo = (token) => {
  return request({
    url: '/user/info',
    method: 'get',
    params: { token }
  })
}

userApi.login = (data) => {
  return request({
    url: '/user/login',
    method: 'post',
    data
  })
}

userApi.logout = () => {
  return request({
    url: '/user/logout',
    method: 'post'
  })
}

export default userApi
