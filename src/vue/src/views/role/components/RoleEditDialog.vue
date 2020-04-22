<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:23:55
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="编辑角色"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <RoleCreateOrEditForm v-if="dialogVisible" :role-id="roleId" @cancel="onCancel" @successful="onSuccessful" />
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
