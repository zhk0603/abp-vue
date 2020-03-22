<template>
  <div>登录成功，正在跳转</div>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  name: 'SigninOidc',
  created() {
    this.oidcSignInCallback()
      .then(redirectPath => {
        this.$router.push(redirectPath)
      })
      .catch(err => {
        console.error(err)
        this.$router.push('/signin-oidc-error') // Handle errors any way you want
      })
  },
  methods: {
    ...mapActions('oidc', ['oidcSignInCallback'])
  }
}
</script>
