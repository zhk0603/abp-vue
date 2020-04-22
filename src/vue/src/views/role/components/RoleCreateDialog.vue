<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:23:55
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="新增角色"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <RoleCreateOrEditForm v-if="dialogVisible" :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import RoleCreateOrEditForm from './RoleCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'RoleCreateDialog',
  components: { RoleCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
  }
}
</script>
