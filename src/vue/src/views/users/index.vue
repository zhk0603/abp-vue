<template>
  <div class="app-container-full">
    <div class="app-full-header">
      <el-input
        v-model="query.userName"
        placeholder="客户名称"
        clearable
        class="header-item"
        size="mini"
      />
      <el-button class="header-item-btn" type="primary" size="mini">搜索</el-button>
      <el-button class="header-item-btn" type="primary" size="mini">新增</el-button>
    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        highlight-current-row
      >
        <el-table-column
          prop="userName"
          label="姓名"
          sortable
        />
        <el-table-column
          prop="email"
          label="邮箱地址"
          sortable
        />
        <el-table-column
          prop="phoneNumber"
          label="手机号"
          sortable
        />
      </el-table>
    </div>
    <div class="app-full-footer">
      <el-pagination
        :page-size="20"
        :pager-count="11"
        layout="prev, pager, next"
        :total="1000"
      />
    </div>
  </div>
</template>

<script>
import userApi from '@/api/user'

export default {
  name: 'Index',
  data() {
    return {
      tableData: [],
      query: {
        userName: ''
      }
    }
  },
  created() {
    userApi.list(this.query).then(res => {
      this.tableData = res.items
    })
  }
}
</script>
