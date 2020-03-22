<template>
  <div class="dashboard-container">
    <div class="dashboard-text">name: {{ name }}</div>
    <user-info />

  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import UserInfo from '@/components/UserInfo'
export default {
  name: 'Dashboard',
  components: { UserInfo },
  computed: {
    ...mapGetters('oidc', [
      'oidcUser'
    ]),
    name() {
      if (this.oidcUser) {
        return this.oidcUser.name
      }

      return ''
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }
  &-text {
    font-size: 30px;
    line-height: 46px;
  }
}
</style>
