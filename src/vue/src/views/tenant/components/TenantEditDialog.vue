<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:24:40
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="编辑租户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <TenantEditForm :tenant-id="tenantId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import TenantEditForm from './TenantEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'TenantEditDialog',
  components: { TenantEditForm },
  mixins: [dialogMixin],
  props: {
    tenantId: {
      type: String,
      default: ''
    }
  }
}
</script>
