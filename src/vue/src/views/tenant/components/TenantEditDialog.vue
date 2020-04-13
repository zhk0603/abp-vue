<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-13 14:31:55
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="Edit Tenant"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <TenantCreateOrEditForm :tenantId="tenantId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import TenantCreateOrEditForm from './TenantCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'TenantEditDialog',
  components: { TenantCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    tenantId: {
      type: String,
      default: ''
    }
  }
}
</script>
