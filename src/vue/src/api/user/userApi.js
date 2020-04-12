import request from '@/utils/request'

const userApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.get = (id) => {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.put = (id, body) => {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'put',
    body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.delete = (id) => {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.getList = (params) => {
  return request({
    url: `/api/identity/users`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.post = (body) => {
  return request({
    url: `/api/identity/users`,
    method: 'post',
    body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.getroles = (id) => {
  return request({
    url: `/api/identity/users/${id}/roles`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.putroles = (id, body) => {
  return request({
    url: `/api/identity/users/${id}/roles`,
    method: 'put',
    body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.getbyusername = (userName) => {
  return request({
    url: `/api/identity/users/by-username/${userName}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 14:02:20
 * @version V1.0.0
 */
userApi.getbyemail = (email) => {
  return request({
    url: `/api/identity/users/by-email/${email}`,
    method: 'get'
  })
}

export default userApi
