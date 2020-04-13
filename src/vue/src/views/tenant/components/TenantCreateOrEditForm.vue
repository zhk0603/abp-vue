<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-13 14:31:55
* @version V1.0.0
!-->
<template>
  <section>
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
            prop="adminEmailAddress"
            label="adminEmailAddress"
          >
            <el-input v-model="formData.adminEmailAddress" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="adminPassword"
            label="adminPassword"
          >
            <el-input v-model="formData.adminPassword" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="name"
            label="name"
          >
            <el-input v-model="formData.name" size="mini" clearable />
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
import { viewModel, rules } from './TenantConfig'

export default {
  name: 'TenantCreateOrEditForm',
  mixins: [fromMixin],
  props: {
    isCreate: {
      type: Boolean,
      default: false
    },
    tenantId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      formData: Object.assign({ }, viewModel),
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
    async get() {
      if (this.tenantId) {
        await tenantApi.get(this.tenantId).then(res => {
          this.formData = Object.assign(this.formData, res)
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
            this.formData = Object.assign({}, viewModel)
            this.$refs.from.resetFields()
          })
        } else {
          return false
        }
      })
    },
    doPost() {
      return tenantApi.post(this.formData)
    },
    doPut() {
      return tenantApi.put(this.tenantId, this.formData)
    },
    cancel() {
      this.$emit('cancel')
    }
  }
}
</script>
