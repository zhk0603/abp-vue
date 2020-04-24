<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:24:40
* @version V1.0.0
!-->
<template>
  <div class="app-full-container">
    <div class="app-full-header">

      <el-input
        v-model="query.filter"
        placeholder="Filter"
        clearable
        class="header-item"
        size="mini"
      />
      <el-button class="header-item-btn" type="primary" size="mini" @click="getList">搜索</el-button>
      <el-button
        v-permission="['AbpTenantManagement.Tenants.Create']"
        class="header-item-btn"
        type="success"
        size="mini"
        @click="create"
      >新增</el-button>
    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        highlight-current-row
        size="small"
        @sort-change="onSortChange"
      >
        <el-table-column
          prop="id"
          label="租户ID"
          sortable="custom"
        />
        <el-table-column
          prop="name"
          label="租户名称"
          sortable="custom"
        />
        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-link
              v-permission="['AbpTenantManagement.Tenants.Update']"
              type="primary"
              icon="el-icon-edit"
              :underline="false"
              @click="edit(scope.row)"
            />
            <el-popconfirm
              v-permission="['AbpTenantManagement.Tenants.Delete']"
              placement="top"
              title="确定删除此项？"
              @onConfirm="del(scope.row)"
            >
              <el-link slot="reference" type="danger" :underline="false" icon="el-icon-delete" />
            </el-popconfirm>
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
      :tenant-id="editTenantId"
      :close-confirm="true"
      dialog-width="500px"
      @close="dialogClose"
    />

  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import tenantApi from '@/api/tenant'
import Pagination from '@/components/Pagination'
import CreateDialog from './components/TenantCreateDialog'
import EditDialog from './components/TenantEditDialog'

export default {
  name: 'Index',
  components: { CreateDialog, EditDialog, Pagination },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      editTenantId: '',
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
      tenantApi.getList(this.query).then(res => {
        this.tableData = res.items
        this.updateTotalCount(res.totalCount)
      })
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
      this.editTenantId = null
    },
    create() {
      this.createDialogVisible = true
    },
    edit(row) {
      this.editTenantId = row.id
      this.editDialogVisible = true
    },
    del(row) {
      tenantApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    }
  }
}
</script>
