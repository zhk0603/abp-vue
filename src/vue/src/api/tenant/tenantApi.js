import request from '@/utils/request'

const tenantApi = {}


/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.get = (id) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.put = (id, body) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'put',
    body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.delete = (id) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'delete'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.getList = (params) => {
  return request({
    url: `/api/multi-tenancy/tenants`,
    method: 'get',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.post = (body) => {
  return request({
    url: `/api/multi-tenancy/tenants`,
    method: 'post',
    body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.getDefaultConnectionString = (id) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.putDefaultConnectionString = (id, params) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'put',
    params
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-12 16:28:49
 * @version V1.0.0
 */
tenantApi.deleteDefaultConnectionString = (id) => {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'delete'
  })
}


export default tenantApi