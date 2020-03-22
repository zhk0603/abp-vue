/**
 * @return  生产环境使用懒加载
 */
module.exports = file => () => import('@/views/' + file)
