<template>
  <section>
    <el-checkbox v-model="grantAll" :indeterminate="isIndeterminate" @change="handleCheckAllChange">授予所有权限</el-checkbox>
    <el-divider />
    <el-tree
      ref="tree"
      show-checkbox
      default-expand-all
      node-key="id"
      :data="treeData"
      :expand-on-click-node="false"
      :check-strictly="true"
      :check-on-click-node="true"
      :default-checked-keys="checkedMenus"
      :props="defaultProps"
      @check-change="onCheckChange"
    >
      <span slot-scope="{ node, data }" class="custom-tree-node">
        <span>
          {{ node.label }}
          <span v-if="enableDisableFeature">
            {{ data.grantedProviders && data.grantedProviders.length > 0 ? `(${data.grantedProviders[0].name})` : "" }}
          </span>
        </span>
      </span>
    </el-tree>
    <el-divider />
    <div class="from-footer">
      <el-button size="mini" @click="cancel">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </section>
</template>

<script>
import menuGrantApi from '@/api/menuGrant'

export default {
  name: 'MenuGrantForm',
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
      treeData: [],
      allMenuIds: [],
      checkedMenus: [],
      defaultProps: {
        children: 'children',
        label: 'displayName'
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
        this.bindData()
      }
    }
  },
  methods: {
    handleCheckAllChange(check) {
      if (check) {
        this.allMenuIds.forEach(x => {
          this.setChecked(x, true, false)
        })
      } else {
        const checkedKeys = this.$refs['tree'].getCheckedKeys()
        checkedKeys.forEach(x => {
          this.setChecked(x, false, false)
        })
      }
      this.isIndeterminate = false
    },
    async bindData() {
      if (this.providerKey) {
        // const treeData = (await menuApi.getList()).items

        // 读取拥有的权限，初始化选中
        menuGrantApi.get({
          providerName: this.providerName,
          providerKey: this.providerKey
        }).then(res => {
          res.menuGrants.forEach(item => {
            this.allMenuIds.push(item.id)
            if (item.isGranted) {
              this.checkedMenus.push(item.id)
            }
          })
          this.$refs['tree'].setCheckedKeys(this.checkedMenus)
          this.initTreeData(res.menus, res.menuGrants)
        })
      }
    },
    cancel() {
      this.$emit('cancel')
    },
    submitForm() {
      const postData = []
      this.flattenData(postData, this.treeData)

      var checkedKeys = this.$refs['tree'].getCheckedKeys()
      postData.forEach(x => {
        x.isGranted = checkedKeys.indexOf(x.id) > -1
      })

      menuGrantApi.put({
        providerName: this.providerName,
        providerKey: this.providerKey
      }, {
        menus: postData
      }).then(() => {
        this.$message('提交成功')
        this.$emit('successful')
      })
    },
    initTreeData(treeData, items) {
      treeData.forEach(x => {
        if (this.enableDisableFeature) {
          const arr = items.filter(y => y.id === x.id)
          if (arr.length > 0) {
            const provoders = arr[0].grantedProviders
            let disabled = provoders.length !== 0
            provoders.forEach(gp => {
              if (gp.name === 'U') {
                disabled = false
              }
            })
            x['disabled'] = disabled
            x['grantedProviders'] = provoders
          }
        }

        this.initChildren(x.children, items)
      })

      this.treeData = treeData
    },
    initChildren(treeData, items) {
      treeData.forEach(x => {
        if (this.enableDisableFeature) {
          const arr = items.filter(y => y.id === x.id)
          if (arr.length > 0) {
            const provoders = arr[0].grantedProviders
            let disabled = provoders.length !== 0
            provoders.forEach(gp => {
              if (gp.name === 'U') {
                disabled = false
              }
            })
            x['disabled'] = disabled
            x['grantedProviders'] = arr[0].grantedProviders
          }
        }

        this.initChildren(x.children, items)
      })
    },
    flattenData(res, menus) {
      for (let i = 0; i < menus.length; i++) {
        const menu = menus[i]
        res.push({
          id: menu.id,
          permissionKey: menu.permissionKey,
          isGranted: false
        })
        this.flattenData(res, menu.children)
      }
    },
    onCheckChange(data, check) {
      if (check && data.parentId) {
        // 将parent设置为true.
        this.setChecked(data.parentId, true)
      } else if (!check && data.children.length > 0) {
        // 被取消时也将 child 设置为 false
        data.children.forEach(x => {
          this.setChecked(x.id, false)
        })
      }

      const checkedLen = this.$refs['tree'].getCheckedKeys().length
      this.grantAll = checkedLen === this.allMenuIds.length
      this.isIndeterminate = checkedLen > 0 && checkedLen < this.allMenuIds.length
    },
    setChecked(key, checked = false, deep = false) {
      const tree = this.$refs['tree']
      const node = tree.getNode(key)
      if (node.disabled !== true) {
        this.$refs['tree'].setChecked(key, checked, deep)
      }
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
