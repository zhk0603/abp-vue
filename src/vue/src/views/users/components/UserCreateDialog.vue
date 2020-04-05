<template>
  <el-dialog
    title="新增用户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <user-create-or-edit-form :roles="roles" :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import UserCreateOrEditForm from './UserCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'UserCreateDialog',
  components: { UserCreateOrEditForm },
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
