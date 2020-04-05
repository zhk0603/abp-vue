<template>
  <el-dialog
    title="提示"
    :show-close="false"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :visible="dialogVisible"
  >
    <span>会话已经过期，请重新登陆</span>
    <div slot="footer" class="dialog-footer">
      <el-button type="primary" size="mini" @click="login">重新登陆</el-button>
    </div>
  </el-dialog>
</template>

<script>
import store from '@/store'

export default {
  name: 'UnauthorizedDialog',
  computed: {
    dialogVisible() {
      return store.state.app.unauthorizedDialogVisible
    }
  },
  methods: {
    login() {
      store.dispatch('app/toggleUnauthorizedDialogVisible', false)
      store.dispatch('oidc/authenticateOidc')
    }
  }
}
</script>
