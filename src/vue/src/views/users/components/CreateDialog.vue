<template>
  <el-dialog
    title="新增用户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <create-or-edit-from @closeDialog="closeDialog" />
  </el-dialog>
</template>

<script>
import CreateOrEditFrom from './CreateOrEditFrom'
import dialogMixin from '@/mixins/dialogMixin'
export default {
  name: 'CreateDialog',
  components: { CreateOrEditFrom },
  mixins: [dialogMixin]
}
</script>
