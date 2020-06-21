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
            label="唯一标识"
          >
            <el-input v-model="formData.name" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="displayName"
            label="显示名称"
          >
            <el-input v-model="formData.displayName" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="componentPath"
            label="组件路径"
          >
            <el-input v-model="formData.componentPath" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="routerPath"
            label="路由路径"
          >
            <el-input v-model="formData.routerPath" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="parentId"
            label="上级菜单"
          >
            <el-popover
              width="500"
              trigger="click"
            >
              <CommonTreeSelector
                node-key="id"
                :default-checked-keys="[formData.parentId]"
                :data="allMenu"
                :props="{label:'displayName'}"
                :height="300"
                @checkChange="onCheckChange"
              />
              <el-input slot="reference" v-model="formData.parentDisplayName" class="form-item" size="mini" readonly />
            </el-popover>
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
            label="排序"
          >
            <el-input v-model="formData.sort" class="form-item" size="mini" clearable />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="permissionKey"
            label="权限"
          >
            <PermissionSelector :value.sync="formData.permissionKey" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            prop="multiTenancySide"
            label="多租户"
          >
            <el-select v-model="formData.multiTenancySide" class="form-item" placeholder="请选择">
              <el-option
                v-for="item in multiTenancySides"
                :key="item.value"
                :label="item.text"
                :value="item.value"
              />
            </el-select>
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
import { menuViewModel, rules, multiTenancySides } from './MenuConfig'
import CommonTreeSelector from '@/components/CommonTreeSelector'
import PermissionSelector from './PermissionSelector'

export default {
  name: 'MenuCreateOrEditForm',
  components: {
    CommonTreeSelector,
    PermissionSelector
  },
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
      allMenu: [],
      formData: Object.assign({}, menuViewModel),
      rules,
      multiTenancySides
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
  created() {
    this.bindData()
  },
  methods: {
    get() {
      if (this.menuId) {
        menuApi.get(this.menuId).then(res => {
          this.formData = Object.assign(this.formData, res)
        })
      }
    },
    bindData() {
      menuApi.getList({ type: 0 }).then(res => {
        this.allMenu = res.items
      })
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
            this.formData = Object.assign({}, menuViewModel)
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
    },
    onCheckChange(nodes) {
      if (nodes.length === 0) {
        this.formData.parentId = null
        this.formData.parentDisplayName = null
      } else {
        if (nodes[0].id === this.formData.id) {
          this.$message({
            message: '不能选择自己作为上级菜单',
            type: 'warning'
          })
          return
        }
        this.formData.parentId = nodes[0].id
        this.formData.parentDisplayName = nodes[0].displayName
      }
    }
  }
}
</script>
