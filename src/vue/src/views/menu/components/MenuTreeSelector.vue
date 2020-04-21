<!--
* @description 菜单选择器
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <section class="tree-container" :style="{height:height + 'px'}">
    <!-- <section class="header">
      <span>请选择</span>
      <el-button type="primary" size="mini" @click="onClick">确定</el-button>
    </section>
    <el-divider class="el-divider" /> -->
    <section class="body">
      <el-tree
        ref="tree"
        :data="data"
        :props="props"
        :default-checked-keys="defaultCheckedKeys"
        :expand-on-click-node="false"
        :check-strictly="true"
        :check-on-click-node="true"
        :node-key="nodeKey"
        show-checkbox
        default-expand-all
        @check-change="onCheckChange"
      />
    </section>
  </section>
</template>

<script>
export default {
  name: 'MenuTreeSelector',
  props: {
    data: {
      type: Array,
      default: function() {
        return []
      }
    },
    nodeKey: {
      type: String,
      default: 'id'
    },
    /**
     * 是否启用多选
     */
    multiple: {
      type: Boolean,
      default: false
    },
    props: {
      type: Object,
      default() {
        return {
          children: 'children',
          label: 'label',
          disabled: 'disabled'
        }
      }
    },
    defaultCheckedKeys: {
      type: Array,
      default: function() {
        return []
      }
    },
    height: {
      type: Number,
      default: 200
    }
  },
  data() {
    return {
    }
  },
  methods: {
    onCheckChange(data) {
      const tree = this.$refs['tree']
      const chekckedKeys = tree.getCheckedKeys()
      if (chekckedKeys.length > 1 && !this.multiple) {
        tree.setCheckedKeys([])
        tree.setChecked(data, true, false)
      }

      this.$emit('checkChange', tree.getCheckedNodes())
    },
    onClick() {
      const tree = this.$refs['tree']
      const chekckedKeys = tree.getCheckedKeys()
      this.$emit('Changed', chekckedKeys)
    }
  }
}
</script>

<style lang="scss" scoped>
.tree-container{
  display:flex;
  flex-direction: column;
  .header{
    height: 50px;
    padding: 0 0 12px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
  }

  .el-divider{
    margin:0 !important;
  }

  .body{
    overflow-y: scroll;
  }

}
</style>
