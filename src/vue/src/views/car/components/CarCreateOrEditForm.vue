<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-27 15:52:05
* @version V1.0.0
!-->
<template>
  <section>
    <el-form
      ref="from"
      :model="formData"
      :rules="rules"
      label-width="120px"
      label-position="top"
      size="mini"
    >
      <el-row>

      </el-row>
    </el-form>
    <div class="from-footer">
      <el-button size="mini" @click="cancel">取消</el-button>
      <el-button type="primary" size="mini" @click="submitForm">提交</el-button>
    </div>
  </section>
</template>

<script>
import fromMixin from '@/mixins/formMixin'
import carApi from '@/api/car'
import { viewModel, rules } from './CarConfig'

export default {
  name: 'CarCreateOrEditForm',
  mixins: [fromMixin],
  props: {
    isCreate: {
      type: Boolean,
      default: false
    },
    carId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      formData: Object.assign({ }, viewModel),
      rules
    }
  },
  watch: {
    carId: {
      immediate: true,
      handler: function() {
        this.get()
      }
    }
  },
  methods: {
    get() {
      if (this.carId) {
        carApi.get(this.carId).then(res => {
          this.formData = Object.assign(this.formData, res)
        })
      }
    },
    submitForm() {
      this.$refs.from.validate((valid) => {
        if (valid) {
          let action = null
          if (this.isCreate) {
            action = this.doPost()
          } else {
            action = this.doPut()
          }

          action.then(() => {
            this.$message('提交成功')
            this.$emit('successful')
            this.formData = Object.assign({}, viewModel)
            this.$refs.from.resetFields()
          })
        } else {
          return false
        }
      })
    },
    doPost() {
      return carApi.post(this.formData)
    },
    doPut() {
      return carApi.put(this.carId, this.formData)
    },
    cancel() {
      this.$refs.from.resetFields()
      this.$emit('cancel')
    }
  }
}
</script>
