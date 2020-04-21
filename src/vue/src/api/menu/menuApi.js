import request from '@/utils/request'

const menuApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-21 19:50:43
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
 * @date 2020-04-21 19:50:43
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
 * @date 2020-04-21 19:50:43
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
 * @date 2020-04-21 19:50:43
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
 * @date 2020-04-21 19:50:43
 * @version V1.0.0
 */
menuApi.getAll = (params) => {
  return request({
    url: `/api/menus/all`,
    method: 'get',
    params
  })
}

export default menuApi
