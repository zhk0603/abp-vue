<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="创建菜单"
    class="dialog"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <MenuCreateOrEditForm :is-create="true" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import MenuCreateOrEditForm from './MenuCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'MenuCreateDialog',
  components: { MenuCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
  }
}
</script>

<style lang="scss" scoped>
.dialog {
  /deep/ .el-dialog__body {
    padding: 30px 20px 10px 20px !important;
  }
}
</style>
