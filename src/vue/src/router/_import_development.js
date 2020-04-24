/**
 * @return  开发环境使用全量默认加载
 */
module.exports = file => require('@/views/' + file).default
