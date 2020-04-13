<template>
  <el-dialog
    title="Edit Tenant"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <TenantCreateOrEditForm :tenant-id="tenantId" @cancel="onCancel" @successful="onSuccessful" />
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
