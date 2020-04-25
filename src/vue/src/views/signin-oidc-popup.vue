<template>
  <div>登录成功，正在跳转</div>
</template>
<script>
import { mapActions } from 'vuex'
export default {
  name: 'SigninOidcPopup',
  created() {
    this.oidcSignInPopupCallback()
      .catch((err) => {
        console.error(err) // Handle errors any way you want
      })
  },
  methods: {
    ...mapActions('oidc', [
      'oidcSignInPopupCallback'
    ])
  }
}
</script>
