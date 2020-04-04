const dialogMixin = {
  props: {
    /**
     * 弹窗是否可见
     */
    visible: {
      type: Boolean,
      default: false
    },
    /**
       * 关闭是否需要确认
      */
    closeConfirm: {
      type: Boolean,
      default: false
    },
    /**
     * dialog width.
     */
    dialogWidth: {
      type: String,
      default: '50%'
    }
  },
  data() {
    return {
      closeConfirmText: '确认关闭？'
    }
  },
  computed: {
    dialogVisible() {
      return this.visible
    }
  },
  methods: {
    beforeClose(done) {
      if (this.closeConfirm) {
        this.$confirm(this.closeConfirmText)
          .then(() => {
            done()
          })
          .catch(() => {})
      } else {
        done()
      }
    },
    closeDialog() {
      this.updateVisible(false)
    },
    updateVisible(visible) {
      this.$emit('update:visible', visible)
    }
  }
}

export default dialogMixin
