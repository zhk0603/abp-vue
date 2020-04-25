<!--
* @description Created by AbpVueCli
* @author zhaokun
* @date 2020-04-20 15:23:55
* @version V1.0.0
!-->
<template>
  <div class="app-full-container">
    <div class="app-full-header">

      <el-button
        v-permission="['AbpIdentity.Roles.Create']"
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
          prop="name"
          label="名称"
          sortable="custom"
        />

        <el-table-column
          prop="isDefault"
          label="是否默认"
          sortable="custom"
        >
          <template slot-scope="scope">
            {{ scope.row.isDefault | formatBoolean }}
          </template>
        </el-table-column>

        <el-table-column
          prop="isPublic"
          label="是否公开"
          sortable="custom"
        >
          <template slot-scope="scope">
            {{ scope.row.isPublic | formatBoolean }}
          </template>
        </el-table-column>

        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-link
              v-permission="['AbpIdentity.Roles.Update']"
              type="primary"
              icon="el-icon-edit"
              :underline="false"
              @click="edit(scope.row)"
            />
            <el-link
              v-permission="['AbpIdentity.Roles.ManagePermissions']"
              type="primary"
              icon="el-icon-setting"
              :underline="false"
              @click="permissionGrant(scope.row)"
            />
            <el-popconfirm
              v-if="!scope.row.isStatic"
              v-permission="['AbpIdentity.Roles.Delete']"
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
      :role-id="editRoleId"
      :close-confirm="true"
      dialog-width="500px"
      @close="dialogClose"
    />
    <MenuGrant
      :visible.sync="permissionGrantDialogVisible"
      :close-confirm="true"
      :provider-key="permissionGrantProviderKey"
      provider-name="R"
      :name="permissionGrantName"
      dialog-width="700px"
      @close="dialogClose"
    />
  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import roleApi from '@/api/role'
import Pagination from '@/components/Pagination'
import CreateDialog from './components/RoleCreateDialog'
import EditDialog from './components/RoleEditDialog'
import MenuGrant from '@/components/MenuGrant'

export default {
  name: 'Index',
  components: { CreateDialog, EditDialog, Pagination, MenuGrant },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      permissionGrantDialogVisible: false,
      editRoleId: '',
      permissionGrantName: '',
      permissionGrantProviderKey: '',
      query: {

      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      roleApi.getList(this.query).then(res => {
        this.tableData = res.items
        this.updateTotalCount(res.totalCount)
      })
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
      this.editRoleId = null
    },
    create() {
      this.createDialogVisible = true
    },
    edit(row) {
      this.editRoleId = row.id
      this.editDialogVisible = true
    },
    del(row) {
      roleApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    },
    permissionGrant(row) {
      this.permissionGrantProviderKey = row.name
      this.permissionGrantName = row.name
      this.permissionGrantDialogVisible = true
    }
  }
}
</script>
