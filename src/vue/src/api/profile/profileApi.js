import request from '@/utils/request'

const profileApi = {}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:21:14
 * @version V1.0.0
 */
profileApi.getList = () => {
  return request({
    url: `/api/identity/my-profile`,
    method: 'get'
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:21:14
 * @version V1.0.0
 */
profileApi.put = (body) => {
  return request({
    url: `/api/identity/my-profile`,
    method: 'put',
    data: body
  })
}

/**
 * Created by AbpVueCli
 * @author zhaokun
 * @date 2020-04-27 10:21:14
 * @version V1.0.0
 */
profileApi.postChangePassword = (body) => {
  return request({
    url: `/api/identity/my-profile/change-password`,
    method: 'post',
    data: body
  })
}

export default profileApi
