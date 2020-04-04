<template>
  <el-dialog
    title="编辑用户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    @close="closeDialog"
  >
    <create-or-edit-from :user-id="userId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import CreateOrEditFrom from './CreateOrEditFrom'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'EditDialog',
  components: { CreateOrEditFrom },
  mixins: [dialogMixin],
  props: {
    userId: {
      type: String,
      default: ''
    }
  }
}
</script>
