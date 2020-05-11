<!--
* @description 权限选择器
* @author zhaokun
* @date 2020-04-21 19:51:28
* @version V1.0.0
!-->
<template>
  <el-select
    v-model="selVal"
    filterable
    clearable
    class="form-item"
    placeholder="请选择"
    @change="onChange"
  >
    <el-option-group
      v-for="group in options"
      :key="group.name"
      :label="group.displayName"
    >
      <el-option
        v-for="item in group.children"
        :key="item.name"
        :label="item.displayName + '('+item.name+ ')'"
        :value="item.name"
      />
    </el-option-group>
  </el-select>
</template>

<script>
import menuApi from '@/api/menu'

export default {
  name: 'PermissionSelector',
  props: {
    value: {
      type: String,
      default: ''
    },
    filterGroup: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      selVal: this.value,
      options: []
    }
  },
  watch: {
    value: function(val) {
      this.selVal = val
    }
  },
  created() {
    this.bindData()
  },
  methods: {
    bindData: function() {
      menuApi.getAuthPolicies().then(res => {
        if (this.filterGroup) {
          this.options = res.filter(x => {
            x.name === this.filterGroup
          })
        } else {
          this.options = res
        }
      })
    },
    onChange(val) {
      this.$emit('update:value', val)
    }
  }

}
</script>
