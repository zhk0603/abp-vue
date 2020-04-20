export const createViewModel = {
  'adminEmailAddress': '',
  'adminPassword': '',
  'name': ''
}

export const editViewModel = {
  'adminEmailAddress': '',
  'adminPassword': '',
  'name': ''
}

export const rules = {
  'adminEmailAddress': [
    {
      'required': true,
      'message': '请输入 管理员邮箱地址',
      'trigger': 'blur'
    }
  ],
  'adminPassword': [
    {
      'required': true,
      'message': '请输入 管理员密码',
      'trigger': 'blur'
    }
  ],
  'name': [
    {
      'required': true,
      'message': '请输入 租户名称',
      'trigger': 'blur'
    },
    {
      'min': 0,
      'max': 64,
      'message': '长度在 0 到 64 个字符',
      'trigger': 'blur'
    }
  ]
}
