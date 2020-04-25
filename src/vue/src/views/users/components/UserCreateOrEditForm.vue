<template>
  <section>
    <el-tabs v-model="activeTabName">
      <el-tab-pane label="用户信息" name="userinfo">
        <el-form
          ref="from"
          :model="formData"
          :rules="rules"
          label-position="top"
          size="mini"
        >
          <el-row>
            <el-col :span="24">
              <el-form-item
                prop="userName"
                label="用户名称"
              >
                <el-input v-model="formData.userName" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="surname"
                label="姓"
              >
                <el-input v-model="formData.surname" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="name"
                label="名"
              >
                <el-input v-model="formData.name" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="password"
                label="密码"
              >
                <el-input v-model="formData.password" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="email"
                label="邮箱地址"
              >
                <el-input v-model="formData.email" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="contractType"
                label="手机号"
              >
                <el-input v-model="formData.phoneNumber" size="mini" clearable />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="lockoutEnabled"
                label="登录失败,账户被锁定"
              >
                <el-switch v-model="formData.lockoutEnabled" />
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item
                prop="twoFactorEnabled"
                label="二次认证"
              >
                <el-switch v-model="formData.twoFactorEnabled" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>

      </el-tab-pane>
      <el-tab-pane label="角色" name="roles">
        <el-form
          size="mini"
        >
          <el-form-item>
            <el-checkbox-group v-model="formData.roleNames">
              <el-checkbox v-for="(item,index) in roles" :key="index" :label="item.name" name="role" />
            </el-checkbox-group>
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-divider />
      <div class="from-footer">
        <el-button size="mini" @click="cancel">取消</el-button>
        <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
      </div>
    </el-tabs>
  </section>
</template>

<script>
import fromMixin from '@/mixins/formMixin'
import userApi from '@/api/user'
import { viewModel, rules } from './UserConfig'

export default {
  name: 'UserCreateOrEditForm',
  mixins: [fromMixin],
  props: {
    isCreate: {
      type: Boolean,
      default: false
    },
    userId: {
      type: String,
      default: ''
    },
    roles: {
      type: Array,
      default: function() {
        return []
      }
    }
  },
  data() {
    return {
      activeTabName: 'userinfo',
      formData: Object.assign({ }, viewModel),
      rules
    }
  },
  watch: {
    userId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    }
  },
  methods: {
    async get() {
      if (this.userId) {
        rules['password'][0].required = false
        const user = await userApi.get(this.userId)
        this.formData = Object.assign(this.formData, user)

        const roles = await userApi.getRoles(this.userId)
        this.formData.roleNames = roles.items.map(x => x.name)
      }
    },
    submitForm() {
      this.$refs.from.validate((valid) => {
        if (valid) {
          let action = null
          if (this.isCreate) {
            action = this.doPost()
          } else {
            action = this.doPut()
          }

          action.then(() => {
            this.$message('提交成功')
            this.$emit('successful')
            this.formData = Object.assign({}, viewModel)
            this.$refs.from.resetFields()
          })
        } else {
          return false
        }
      })
    },
    doPost() {
      return userApi.post(this.formData)
    },
    doPut() {
      return userApi.put(this.userId, this.formData)
    },
    cancel() {
      this.$emit('cancel')
    }
  }
}
</script>
