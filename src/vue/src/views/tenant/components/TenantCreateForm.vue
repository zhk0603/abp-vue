<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:24:40
* @version V1.0.0
!-->
<template>
  <section>
    <el-form
      ref="from"
      :model="formData"
      :rules="rules"
      label-width="120px"
      label-position="top"
      size="mini"
    >
      <el-row>
        <el-col :span="24">
          <el-form-item
            prop="name"
            label="租户名称"
          >
            <el-input v-model="formData.name" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="adminPassword"
            label="管理员密码"
          >
            <el-input v-model="formData.adminPassword" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="adminEmailAddress"
            label="管理员邮箱地址"
          >
            <el-input v-model="formData.adminEmailAddress" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <div class="from-footer">
      <el-button size="mini" @click="cancel">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </section>
</template>

<script>
import fromMixin from '@/mixins/formMixin'
import tenantApi from '@/api/tenant'
import { createViewModel, rules } from './TenantConfig'

export default {
  name: 'TenantCreateForm',
  mixins: [fromMixin],
  props: {
    tenantId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      formData: Object.assign({ }, createViewModel),
      rules
    }
  },
  watch: {
    tenantId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    }
  },
  methods: {
    get() {
      if (this.tenantId) {
        tenantApi.get(this.tenantId).then(res => {
          this.formData = Object.assign(this.formData, res)
        })
      }
    },
    submitForm() {
      this.$refs.from.validate((valid) => {
        if (valid) {
          tenantApi.post(this.formData).then(() => {
            this.$message('提交成功')
            this.$emit('successful')
            this.formData = Object.assign({}, createViewModel)
            this.$refs.from.resetFields()
          })
        } else {
          return false
        }
      })
    },
    cancel() {
      this.$refs.from.resetFields()
      this.$emit('cancel')
    }
  }
}
</script>
