export const menuViewModel = {
  'name': '',
  'displayName': '',
  'componentPath': '',
  'routerPath': '',
  'parentId': '',
  'parentName': '',
  'menuType': 0,
  'icon': '',
  'sort': '',
  'targetUrl': '',
  'permissionKey': ''
}

export const rules = {
  'name': [
    {
      'required': true,
      'message': '请输入名称',
      'trigger': 'blur'
    }
  ],
  'displayName': [
    {
      'required': true,
      'message': '请输入显示名称',
      'trigger': 'blur'
    }
  ],
  'componentPath': [
    {
      'required': true,
      'message': '请输入组件路径',
      'trigger': 'blur'
    }
  ],
  'routerPath': [
    {
      'required': true,
      'message': '请输入路由路径',
      'trigger': 'blur'
    }
  ],
  'menuType': [
    {
      'type': 'integer',
      'message': 'menuType 必须为 integer',
      'trigger': 'change'
    }
  ]
}
