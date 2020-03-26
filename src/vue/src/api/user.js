import request from '@/utils/request'

const userApi = {}

userApi.list = (query) => {
  return request.get('/api/identity/users', query)
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
