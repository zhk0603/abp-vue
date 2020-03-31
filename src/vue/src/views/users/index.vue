<template>
  <div class="app-container-full">
    <div class="app-full-header">
      <el-input
        v-model="query.filter"
        placeholder="客户名称"
        clearable
        class="header-item"
        size="mini"
      />
      <el-button class="header-item-btn" type="primary" size="mini" @click="getList">搜索</el-button>
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
        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-link icon="el-icon-edit">编辑</el-link>
            <el-link>查看<i class="el-icon-view el-icon--right" /> </el-link>
          </template>
        </el-table-column>
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
        filter: ''
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      userApi.list(this.query).then(res => {
        this.tableData = res.items
      })
    }
  }
}
</script>
