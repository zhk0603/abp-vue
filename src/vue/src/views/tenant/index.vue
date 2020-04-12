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

    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        highlight-current-row
        @sort-change="onSortChange"
      >

        <el-table-column
          prop="name"
          label="name"
          sortable="custom"
        />
        <el-table-column
          prop="id"
          label="id"
          sortable="custom"
        />
        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">

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

  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import tenantApi from '@/api/tenant'
import Pagination from '@/components/Pagination'

export default {
  name: 'Index',
  components: { Pagination },
  mixins: [listMixin],
  data() {
    return {

      query: {
        filter: ''
        // 在这里写列表过滤属性
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

    del(row) {
      tenantApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    }
  }
}
</script>
