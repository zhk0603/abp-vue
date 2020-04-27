<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-27 15:52:05
* @version V1.0.0
!-->
<template>
  <div class="app-full-container">
    <div class="app-full-header">


      <el-button class="header-item-btn" type="success" size="mini" @click="create">新增</el-button>
    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        highlight-current-row
        @sort-change="onSortChange"
      >

        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-link type="primary" icon="el-icon-edit" @click="edit(scope.row)">编辑</el-link>
            <el-popconfirm placement="top" title="确定删除此项？" @onConfirm="del(scope.row)">
              <el-link slot="reference" type="danger" icon="el-icon-delete">删除</el-link>
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
      dialog-width="50%"
      @close="dialogClose"
    />
    <EditDialog
      :visible.sync="editDialogVisible"
      :carId="editCarId"
      :close-confirm="true"
      dialog-width="50%"
      @close="dialogClose"
    />

  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import carApi from '@/api/car'
import Pagination from '@/components/Pagination'
import CreateDialog from './components/CarCreateDialog'
import EditDialog from './components/CarEditDialog'

export default {
  name: 'Index',
  components: { CreateDialog, EditDialog, Pagination },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      editCarId: '',
      query: {

      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      carApi.getList(this.query).then(res => {
        this.tableData = res.items
        this.updateTotalCount(res.totalCount)
      })
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
      this.editCarId = null
    },
    create() {
      this.createDialogVisible = true
    },
    edit(row) {
      this.editCarId = row.id
      this.editDialogVisible = true
    },
    del(row) {
      carApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    }
  }
}
</script>
