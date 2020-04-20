<template>
  <section>
    <el-checkbox v-model="grantAll" :indeterminate="isIndeterminate" @change="handleCheckAllChange">授予所有权限</el-checkbox>
    <el-divider />
    <el-tabs tab-position="left">
      <el-tab-pane v-for="(item ,index) in tabData" :key="index" :label="item.displayName">
        <el-tree
          :ref="'tree'+index"
          show-checkbox
          default-expand-all
          node-key="id"
          :data="item.treeData"
          :expand-on-click-node="false"
          :check-strictly="true"
          :check-on-click-node="true"
          :default-checked-keys="item.initPermission"
          :props="defaultProps"
          @check-change="onCheckChange"
        >
          <span slot-scope="{ node, data }" class="custom-tree-node">
            <span>
              {{ node.label }}
              <span v-if="enableDisableFeature">
                {{ data.grantedProviders && data.grantedProviders.length > 0 ? `(${data.grantedProviders[0].providerName})` : "" }}
              </span>
            </span>
          </span>
        </el-tree>
      </el-tab-pane>
    </el-tabs>
    <el-divider />
    <div class="from-footer">
      <el-button size="mini" @click="cancel">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </section>
</template>

<script>
import permissionApi from '@/api/permissions'

export default {
  name: 'PermissionGrantForm',
  props: {
    providerKey: {
      type: String,
      default: ''
    },
    providerName: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      grantAll: false,
      isIndeterminate: true,
      permissions: {},
      tabData: [],
      /**
       * 用于映射权限与分组的关系， 方便查找，
       * 因为在 tree 的回调事件中，无法得知是哪一个 tree 实例
       */
      allPermissionMap: new Map(),
      defaultProps: {
        children: 'children',
        label: 'label'
      }
    }
  },
  computed: {
    /**
     * 是否启用“禁用功能”
     */
    enableDisableFeature: function() {
      return this.providerName === 'U'
    }
  },
  watch: {
    providerKey: {
      immediate: true,
      handler: function() {
        this.getPermissions()
      }
    }
  },
  methods: {
    handleCheckAllChange(check) {
      this.isIndeterminate = false
    },
    getPermissions() {
      if (this.providerKey) {
        permissionApi.getList({
          providerName: this.providerName,
          providerKey: this.providerKey
        }).then(res => {
          this.permissions = res
          this.initTree()
        })
      }
    },
    initTree() {
      this.tabData = this.getTabData()
    },
    getTabData() {
      if (this.permissions.groups) {
        const data = []

        this.permissions.groups.forEach((group, gIndex) => {
          const item = {
            displayName: group.displayName,
            name: group.name,
            treeData: [], // tree 组件的数据
            initPermission: [] // 初始化 tree 选中的node key
          }
          data.push(item)

          // 构造 treeData
          const roots = group.permissions.filter(x => {
            return x.parentName == null
          })
          roots.forEach(x => {
            const tmp = {
              id: x.name,
              label: x.displayName,
              grantedProviders: x.grantedProviders
            }
            if (this.enableDisableFeature) {
              tmp['disabled'] = x.grantedProviders.length !== 0
            }
            item.treeData.push(tmp)

            if (this.enableDisableFeature) {
              x.grantedProviders.forEach(gp => {
                if (gp.providerName === 'U') {
                  tmp.disabled = false
                }
              })
            }
            this.allPermissionMap.set(tmp.id, gIndex)
            if (x.isGranted) {
              item.initPermission.push(x.name)
            }

            const children = group.permissions.filter(c => {
              return c.parentName === x.name
            }).map(y => {
              this.allPermissionMap.set(y.name, gIndex)
              if (y.isGranted) {
                item.initPermission.push(y.name)
              }
              const { name: id, displayName: label, ...rest } = y
              const res = {
                id,
                label,
                ...rest
              }
              if (this.enableDisableFeature) {
                let disabled = y.grantedProviders.length !== 0
                y.grantedProviders.forEach(gp => {
                  if (gp.providerName === 'U') {
                    disabled = false
                  }
                })
                res['disabled'] = disabled
              }

              return res
            })
            tmp.children = children
          })
        })
        return data
      }
      return []
    },
    cancel() {
      this.$emit('cancel')
    },
    submitForm() {
      const checkedKeys = this.getAllTreeCheckedKeys()
      const permissions = []
      for (const key of this.allPermissionMap.keys()) {
        permissions.push({
          name: key,
          isGranted: checkedKeys.indexOf(key) > -1
        })
      }
      permissionApi.put({
        providerName: this.providerName,
        providerKey: this.providerKey
      }, {
        permissions
      }).then(res => {
        this.$message('提交成功')
        this.$emit('successful')
      })
    },
    getAllTreeCheckedKeys() {
      const checkKeys = []
      for (let index = 0; index < this.tabData.length; index++) {
        checkKeys.push(...this.$refs[`tree${index}`][0].getCheckedKeys())
      }
      return checkKeys
    },
    onCheckChange(data, check, childCheck) {
      if (check && data.parentName) {
        // 将parent设置为true.
        this.setCheckedProxy(data.parentName, true)
      } else if (!check) {
        // 被取消时也将 child 设置为 false
        const childKeyPrefix = data.id + '.'; // 这就要求 权限名 与 ‘.’ 分割

        ([...this.allPermissionMap.keys()])
          .filter(x => x.indexOf(childKeyPrefix) > -1)
          .forEach(key => {
            this.setCheckedProxy(key, false)
          })
      }
    },
    setCheckedProxy(key, checked = false, deep = false) {
      const treeIndex = this.allPermissionMap.get(key)
      this.$refs[`tree${treeIndex}`][0].setChecked(key, checked, deep)
    }
  }
}
</script>

<style lang="scss" scoped>
  .custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
  }
</style>
