<template>
  <el-form
    ref="from"
    :model="fromData"
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
          <el-input v-model="fromData.userName" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="surname"
          label="姓"
        >
          <el-input v-model="fromData.surname" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="name"
          label="名"
        >
          <el-input v-model="fromData.name" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="password"
          label="密码"
        >
          <el-input v-model="fromData.password" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="email"
          label="邮箱地址"
        >
          <el-input v-model="fromData.email" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="contractType"
          label="手机号"
        >
          <el-input v-model="fromData.phoneNumber" size="mini" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="lockoutEnabled"
          label="登录失败,账户被锁定"
        >
          <el-switch v-model="fromData.lockoutEnabled" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item
          prop="twoFactorEnabled"
          label="二次认证"
        >
          <el-switch v-model="fromData.twoFactorEnabled" />
        </el-form-item>
      </el-col>

    </el-row>
    <div class="from-footer">
      <el-button size="mini" @click="cancel">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </el-form>

</template>

<script>
import fromMixin from '@/mixins/formMixin'
import userApi from '@/api/user'
import { viewModel, rules } from './config'

export default {
  name: 'CreateOrEditFrom',
  mixins: [fromMixin],
  props: {
    userId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      roles: ['admin', 'abc'],
      fromData: Object.assign({}, viewModel),
      rules
    }
  },
  computed: {
    isCreate() {
      return !this.userId
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
    get() {
      if (this.userId) {
        rules['password'][0].required = false
        userApi.get(this.userId).then(res => {
          this.fromData = Object.assign(this.fromData, res)
        })
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
            this.fromData = Object.assign({}, viewModel)
          })
        } else {
          return false
        }
      })
    },
    doPost() {
      return userApi.post(this.fromData)
    },
    doPut() {
      return userApi.put(this.userId, this.fromData)
    },
    cancel() {
      this.$emit('cancel')
    }
  }
}
</script>
