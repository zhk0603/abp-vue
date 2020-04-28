import request from '@/utils/request'

const userLookupApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:20:09
 * @version V1.0.0
 */
userLookupApi.getLookup = (id) => {
  return request({
    url: `/api/identity/users/lookup/${id}`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:20:09
 * @version V1.0.0
 */
userLookupApi.getLookupByUsername = (userName) => {
  return request({
    url: `/api/identity/users/lookup/by-username/${userName}`,
    method: 'get'
  })
}

export default userLookupApi
