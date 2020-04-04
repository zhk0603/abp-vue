<template>
  <div class="app-full-container">
    <div class="app-full-header">
      <el-input
        v-model="query.filter"
        placeholder="关键字"
        clearable
        class="header-item"
        size="mini"
      />
      <el-button class="header-item-btn" type="primary" size="mini" @click="getList">搜索</el-button>
      <el-button class="header-item-btn" type="success" size="mini" @click="create">新增</el-button>
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
            <el-link type="primary" icon="el-icon-edit" @click="edit(scope.row)">编辑</el-link>
            <el-link type="primary" icon="el-icon-setting">权限</el-link>
            <el-link type="danger" icon="el-icon-delete">删除</el-link>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <div class="app-full-footer">
      <pagination
        :total="pagination.totalCount"
        :page.sync="pagination.pageIndex"
        :limit.sync="query.maxResultCount"
        @pagination="onPagination"
      />
    </div>

    <CreateDialog
      :visible.sync="createDialogVisible"
      :close-confirm="true"
      dialog-width="500px"
      @close="dialogClose"
    />
    <EditDialog
      :visible.sync="editDialogVisible"
      :user-id="editUserid"
      @close="dialogClose"
    />
  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import userApi from '@/api/user'
import CreateDialog from './components/CreateDialog'
import EditDialog from './components/EditDialog'
import Pagination from '@/components/Pagination'

export default {
  name: 'Index',
  components: { CreateDialog, EditDialog, Pagination },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      editUserid: '',

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
        this.updateTotalCount(res.totalCount)
      })
    },
    create() {
      this.createDialogVisible = true
    },
    edit(row) {
      this.editUserid = row.id
      this.editDialogVisible = true
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
    }
  }
}
</script>
