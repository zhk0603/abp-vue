import request from '@/utils/request'

const roleApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.getAll = () => {
  return request({
    url: `/api/identity/roles/all`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.getList = (params) => {
  return request({
    url: `/api/identity/roles`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.post = (body) => {
  return request({
    url: `/api/identity/roles`,
    method: 'post',
    data: body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.get = (id) => {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.put = (id, body) => {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'put',
    data: body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-14 12:18:49
 * @version V1.0.0
 */
roleApi.delete = (id) => {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}

export default roleApi
