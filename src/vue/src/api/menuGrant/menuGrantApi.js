import request from '@/utils/request'

const menuGrantApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 09:57:50
 * @version V1.0.0
 */
menuGrantApi.getList = () => {
  return request({
    url: `/api/menu-grant/list`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 09:57:50
 * @version V1.0.0
 */
menuGrantApi.get = (params) => {
  return request({
    url: `/api/menu-grant`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 09:57:50
 * @version V1.0.0
 */
menuGrantApi.put = (params, body) => {
  return request({
    url: `/api/menu-grant`,
    method: 'put',
    params,
    data: body
  })
}

export default menuGrantApi
