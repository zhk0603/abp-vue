import request from '@/utils/request'

const abpTenantApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:49:56
 * @version V1.0.0
 */
abpTenantApi.getByName = (name) => {
  return request({
    url: `/api/abp/multi-tenancy/tenants/by-name/${name}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:49:56
 * @version V1.0.0
 */
abpTenantApi.getById = (id) => {
  return request({
    url: `/api/abp/multi-tenancy/tenants/by-id/${id}`,
    method: 'get'
  })
}

export default abpTenantApi
