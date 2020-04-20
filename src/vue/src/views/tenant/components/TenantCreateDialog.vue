<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:24:40
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="创建租户"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <TenantCreateForm @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import TenantCreateForm from './TenantCreateForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'TenantCreateDialog',
  components: { TenantCreateForm },
  mixins: [dialogMixin],
  props: {
  }
}
</script>
