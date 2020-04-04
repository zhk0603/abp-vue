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
      <el-button size="mini" @click="closeDialog">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </el-form>

</template>

<script>
import fromMixin from '@/mixins/formMixin'
import rules from './CreateOrEditFormRules'
import userApi from '@/api/user'

export default {
  name: 'CreateOrEditFrom',
  mixins: [fromMixin],
  props: {
    id: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      roles: ['admin', 'abc'],
      fromData: {
        userName: '',
        name: '',
        surname: '',
        password: '',
        email: '',
        phoneNumber: '',
        lockoutEnabled: true,
        twoFactorEnabled: true,
        roleNames: []
      },
      rules
    }
  },
  watch: {
    id: function(val) {
      // todo
    }
  },
  methods: {
    submitForm() {
      this.$refs.from.validate((valid) => {
        if (valid) {
          userApi.post(this.fromData).then(() => {
            this.$message('提交成功')
          })
        } else {
          return false
        }
      })
    },
    closeDialog() {
      this.$emit('closeDialog')
    }
  }
}
</script>
