<template>
  <el-dialog
    :title="'权限 - ' + name"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <permission-grant-form :provider-key="providerKey" :provider-name="providerName" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import PermissionGrantForm from './PermissionGrantForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'PermissionGrant',
  components: { PermissionGrantForm },
  mixins: [dialogMixin],
  props: {
    providerKey: {
      type: String,
      default: ''
    },
    providerName: {
      type: String,
      default: ''
    },
    name: {
      type: String,
      default: ''
    }
  }
}
</script>
