export const viewModel = {
  'adminEmailAddress': '',
  'adminPassword': '',
  'name': ''
}

export const rules = {
  'adminEmailAddress': [
    {
      'required': true,
      'message': '请输入',
      'trigger': 'blur'
    },
    {
      'type': 'string',
      'message': ' 必须为 string',
      'trigger': 'change'
    }
  ],
  'adminPassword': [
    {
      'required': true,
      'message': '请输入',
      'trigger': 'blur'
    },
    {
      'type': 'string',
      'message': ' 必须为 string',
      'trigger': 'change'
    }
  ],
  'name': [
    {
      'required': true,
      'message': '请输入',
      'trigger': 'blur'
    },
    {
      'min': 0,
      'max': 64,
      'message': '长度在 0 到 64 个字符',
      'trigger': 'blur'
    },
    {
      'type': 'string',
      'message': ' 必须为 string',
      'trigger': 'change'
    }
  ]
}
