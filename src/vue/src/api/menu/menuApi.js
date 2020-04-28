import request from '@/utils/request'

const menuApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.get = (id) => {
  return request({
    url: `/api/menus/${id}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.put = (id, body) => {
  return request({
    url: `/api/menus/${id}`,
    method: 'put',
    data: body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.delete = (id) => {
  return request({
    url: `/api/menus/${id}`,
    method: 'delete'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.getList = (params) => {
  return request({
    url: `/api/menus`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.post = (body) => {
  return request({
    url: `/api/menus`,
    method: 'post',
    data: body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:01:45
 * @version V1.0.0
 */
menuApi.getAuthPolicies = () => {
  return request({
    url: `/api/menus/auth-policies`,
    method: 'get'
  })
}

export default menuApi
