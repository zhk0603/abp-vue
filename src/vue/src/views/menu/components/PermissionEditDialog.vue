<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="编辑权限"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <PermissionCreateOrEditForm v-if="dialogVisible" :menu-id="menuId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import PermissionCreateOrEditForm from './PermissionCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'PermissionEditDialog',
  components: { PermissionCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    menuId: {
      type: String,
      default: ''
    }
  }
}
</script>
