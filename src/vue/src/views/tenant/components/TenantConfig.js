export const viewModel = {
  "adminEmailAddress": "",
  "adminPassword": "",
  "name": ""
}

export const rules = {
  "adminEmailAddress": [
    {
      "required": true,
      "message": "请输入adminEmailAddress",
      "trigger": "blur"
    },
    {
      "type": "string",
      "message": "adminEmailAddress 必须为 string",
      "trigger": "change"
    }
  ],
  "adminPassword": [
    {
      "required": true,
      "message": "请输入adminPassword",
      "trigger": "blur"
    },
    {
      "type": "string",
      "message": "adminPassword 必须为 string",
      "trigger": "change"
    }
  ],
  "name": [
    {
      "required": true,
      "message": "请输入name",
      "trigger": "blur"
    },
    {
      "min": 0,
      "max": 64,
      "message": "长度在 0 到 64 个字符",
      "trigger": "blur"
    },
    {
      "type": "string",
      "message": "name 必须为 string",
      "trigger": "change"
    }
  ]
}