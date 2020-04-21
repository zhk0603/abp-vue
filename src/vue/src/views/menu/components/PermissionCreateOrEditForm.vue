<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <section>
    <el-form
      ref="from"
      :model="formData"
      :rules="rules"
      label-width="120px"
      label-position="right"
      size="mini"
    >
      <el-row>

        <el-col :span="12">
          <el-form-item
            prop="name"
            label="name"
          >

            <el-input v-model="formData.name" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="displayName"
            label="displayName"
          >

            <el-input v-model="formData.displayName" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="componentPath"
            label="componentPath"
          >

            <el-input v-model="formData.componentPath" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="routerPath"
            label="routerPath"
          >

            <el-input v-model="formData.routerPath" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="parentId"
            label="parentId"
          >

            <el-input v-model="formData.parentId" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="menuType"
            label="menuType"
          >

            <el-input v-model="formData.menuType" class="form-item" size="mini" type="number" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="icon"
            label="icon"
          >

            <el-input v-model="formData.icon" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="sort"
            label="sort"
          >

            <el-input v-model="formData.sort" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="targetUrl"
            label="targetUrl"
          >

            <el-input v-model="formData.targetUrl" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="permissionKey"
            label="permissionKey"
          >

            <el-input v-model="formData.permissionKey" class="form-item" size="mini" clearable />

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="id"
            label="id"
          >

            <el-input v-model="formData.id" class="form-item" size="mini" clearable />

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
import menuApi from '@/api/menu'
import { viewModel, rules } from './MenuConfig'

export default {
  name: 'PermissionCreateOrEditForm',
  mixins: [fromMixin],
  props: {
    isCreate: {
      type: Boolean,
      default: false
    },
    menuId: {
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
    menuId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    }
  },
  methods: {
    async get() {
      if (this.menuId) {
        await menuApi.get(this.menuId).then(res => {
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
      return menuApi.post(this.formData)
    },
    doPut() {
      return menuApi.put(this.menuId, this.formData)
    },
    cancel() {
      this.$refs.from.resetFields()
      this.$emit('cancel')
    }
  }
}
</script>
