<template>
  <div>redirect-silent-renew</div>
</template>
<script>
import { vuexOidcProcessSilentSignInCallback } from 'vuex-oidc'
import Settings from '@/settings.js'

export default {
  name: 'RedirectSilentRenew',
  created() {
    vuexOidcProcessSilentSignInCallback(Settings.oidcSettings)
  }
}
</script>
