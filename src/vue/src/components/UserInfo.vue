<template>
  <div
    v-if="oidcIsAuthenticated"
  >
    <p>
      You are signed in as:
    </p>
    <div style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" v-html="userDisplay" />
    <p>
      access token
    </p>
    <p>
      expires {{ new Date(oidcIdTokenExp).toISOString() }}
    </p>
    <textarea v-model="oidcAccessToken" readonly style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" />

    <p>
      <button @click="authenticateOidcSilent">Reauthenticate silently</button>
    </p>

  </div>
  <div v-else-if="oidcAuthenticationIsChecked">
    <p>You are not signed in</p>
    <el-button @click="authenticateOidc">登录</el-button>
  </div>
  <p v-else>Silent renew is in progress...</p>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import jsonMarkup from 'json-markup'

export default {
  name: 'UserInfo',
  computed: {
    ...mapGetters('oidc', [
      'oidcIsAuthenticated',
      'oidcAuthenticationIsChecked',
      'oidcUser',
      'oidcAccessToken',
      'oidcIdTokenExp'
    ]),
    userDisplay: function() {
      return jsonMarkup(this.oidcUser)
    }
  },
  methods: {
    ...mapActions('oidc', ['authenticateOidc', 'authenticateOidcSilent'])
  }
}
</script>

<style>
.json-markup {
  color: transparent;
}
.json-markup span {
  color: black;
  float: left;
}
.json-markup .json-markup-key {
  clear: left;
}
</style>
