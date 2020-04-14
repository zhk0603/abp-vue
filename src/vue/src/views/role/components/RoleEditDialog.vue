<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-14 12:18:49
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="Edit Role"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <RoleCreateOrEditForm :roleId="roleId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import RoleCreateOrEditForm from './RoleCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'RoleEditDialog',
  components: { RoleCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    roleId: {
      type: String,
      default: ''
    }
  }
}
</script>
