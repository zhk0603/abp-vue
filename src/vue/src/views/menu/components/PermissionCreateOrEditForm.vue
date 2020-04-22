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
      :rules="permissionRules"
      label-width="120px"
      label-position="right"
      size="mini"
    >
      <el-row>
        <el-col :span="24">
          <el-form-item
            prop="parentDisplayName"
            label="上级菜单"
          >
            <el-input v-model="formData.parentDisplayName" class="form-item" size="mini" disabled />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="displayName"
            label="名称"
          >
            <el-input v-model="formData.displayName" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            prop="permissionKey"
            label="权限"
          >
            <el-input v-model="formData.permissionKey" class="form-item" size="mini" clearable />
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
import { permissionViewModel, permissionRules } from './MenuConfig'

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
    },
    parentMenu: {
      type: Object,
      default: function() {
        return {}
      }
    }
  },
  data() {
    return {
      formData: Object.assign({ }, permissionViewModel),
      permissionRules
    }
  },
  watch: {
    menuId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    },
    parentMenu: {
      deep: true,
      immediate: true,
      handler: function(val) {
        // 将parentMenu的值，填充到formData
        const {
          id: parentId,
          displayName: parentDisplayName
        } = val
        this.formData = Object.assign(this.formData, { parentId, parentDisplayName })
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
            this.formData = Object.assign({}, permissionViewModel)
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
