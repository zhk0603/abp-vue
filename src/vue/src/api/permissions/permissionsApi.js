import request from '@/utils/request'

const permissionsApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 09:56:59
 * @version V1.0.0
 */
permissionsApi.getList = (params) => {
  return request({
    url: `/api/abp/permissions`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 09:57:02
 * @version V1.0.0
 */
permissionsApi.put = (params, body) => {
  return request({
    url: `/api/abp/permissions`,
    method: 'put',
    params,
    data: body
  })
}

export default permissionsApi
