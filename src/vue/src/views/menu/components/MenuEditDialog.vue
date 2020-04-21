<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <el-dialog
    title="Edit Menu"
    :visible="dialogVisible"
    :before-close="beforeClose"
    :width="dialogWidth"
    @close="closeDialog"
  >
    <MenuCreateOrEditForm :menu-id="menuId" @cancel="onCancel" @successful="onSuccessful" />
  </el-dialog>
</template>

<script>
import MenuCreateOrEditForm from './MenuCreateOrEditForm'
import dialogMixin from '@/mixins/dialogMixin'

export default {
  name: 'MenuEditDialog',
  components: { MenuCreateOrEditForm },
  mixins: [dialogMixin],
  props: {
    menuId: {
      type: String,
      default: ''
    }
  }
}
</script>
