<template>
  <div
    v-if="oidcIsAuthenticated"
  >
    <p>
      You are signed in as:
    </p>
    <div style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" v-html="userDisplay" />
    <p>
      Id token
    </p>
    <p>
      expires {{ new Date(oidcIdTokenExp).toISOString() }}
    </p>
    <textarea v-model="oidcIdToken" readonly style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" />

    <p>
      <button @click="authenticateOidcSilent">Reauthenticate silently</button>
    </p>

  </div>
  <p v-else-if="oidcAuthenticationIsChecked">You are not signed in</p>
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
      'oidcIdToken',
      'oidcIdTokenExp'
    ]),
    userDisplay: function() {
      return jsonMarkup(this.oidcUser)
    }
  },
  methods: {
    ...mapActions('oidc', ['authenticateOidcSilent'])
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
