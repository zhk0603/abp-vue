<template>
  <el-dialog
    title="编辑用户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <user-create-or-edit-form :user-id="userId" :roles="roles" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import UserCreateOrEditForm from './UserCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'UserEditDialog',
  components: { UserCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    userId: {
      type: String,
      default: ''
    },
    roles: {
      type: Array,
      default: function() {
        return []
      }
    }
  }
}
</script>
