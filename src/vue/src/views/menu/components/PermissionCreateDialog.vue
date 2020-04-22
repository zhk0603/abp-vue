<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="创建权限"
    class="dialog"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <PermissionCreateOrEditForm v-if="dialogVisible" :parent-menu="parentMenu" :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import PermissionCreateOrEditForm from './PermissionCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'PermissionCreateDialog',
  components: { PermissionCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    parentMenu: {
      type: Object,
      default: function() {
        return {}
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.dialog {
  /deep/ .el-dialog__body {
    padding: 30px 20px 10px 20px !important;
  }
}
</style>
