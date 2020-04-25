<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:23:55
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
            label="角色名称"
          >
            <el-input v-model="formData.name" class="form-item" size="mini" :disabled="formData.isStatic" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">

          <el-form-item
            prop="isDefault"
            label="默认"
          >

            <el-switch v-model="formData.isDefault" />

          </el-form-item>
        </el-col>
        <el-col :span="24">

          <el-form-item
            prop="isPublic"
            label="公开"
          >

            <el-switch v-model="formData.isPublic" />

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
import roleApi from '@/api/role'
import { viewModel, rules } from './RoleConfig'

export default {
  name: 'RoleCreateOrEditForm',
  mixins: [fromMixin],
  props: {
    isCreate: {
      type: Boolean,
      default: false
    },
    roleId: {
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
    roleId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    }
  },
  methods: {
    get() {
      if (this.roleId) {
        roleApi.get(this.roleId).then(res => {
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
      return roleApi.post(this.formData)
    },
    doPut() {
      return roleApi.put(this.roleId, this.formData)
    },
    cancel() {
      this.$refs.from.resetFields()
      this.$emit('cancel')
    }
  }
}
</script>
