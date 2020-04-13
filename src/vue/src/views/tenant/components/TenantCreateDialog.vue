<template>
  <el-dialog
    title="Create Tenant"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <TenantCreateOrEditForm :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import TenantCreateOrEditForm from './TenantCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'TenantCreateDialog',
  components: { TenantCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
  }
}
</script>
