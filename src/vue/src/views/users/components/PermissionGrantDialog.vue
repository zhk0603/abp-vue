<template>
  <el-dialog
    :title="'权限 - ' + name"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <permission-grant-form :user-id="userId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import PermissionGrantForm from './PermissionGrantForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'PermissionGrantDialog',
  components: { PermissionGrantForm },
  mixins: [dialogMixin],
  props: {
    userId: {
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
