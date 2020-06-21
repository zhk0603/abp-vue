export const menuViewModel = {
  'name': '',
  'displayName': '',
  'componentPath': '',
  'routerPath': '',
  'parentId': '',
  'parentDisplayName': '',
  'menuType': 0,
  'icon': '',
  'sort': '',
  'targetUrl': '',
  'permissionKey': '',
  'multiTenancySide': 3
}

export const rules = {
  'name': [
    {
      'required': true,
      'message': '请输入 唯一标识',
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

export const permissionViewModel = {
  'name': '',
  'displayName': '',
  'parentId': '',
  'parentDisplayName': '',
  'menuType': 1,
  'permissionKey': '',
  'multiTenancySide': 3
}

export const permissionRules = {
  'displayName': [
    {
      'required': true,
      'message': '请输入 名称',
      'trigger': 'blur'
    }
  ]
}

export const multiTenancySides = [{
  value: 1,
  text: '租户'
}, {
  value: 2,
  text: '宿主'
}, {
  value: 3,
  text: '宿主和租户'
}]
