<template>
  <el-dialog
    title="新增用户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <create-or-edit-from :roles="roles" :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import CreateOrEditFrom from './CreateOrEditFrom'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'CreateDialog',
  components: { CreateOrEditFrom },
  mixins: [dialogMixin],
  props: {
    roles: {
      type: Array,
      default: function() {
        return []
      }
    }
  }
}
</script>
