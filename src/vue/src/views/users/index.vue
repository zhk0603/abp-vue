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
      <el-button
        v-permission="['AbpIdentity.Users.Create']"
        class="header-item-btn"
        type="success"
        size="mini"
        @click="create"
      >新增</el-button>
    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        :default-sort="{prop: 'creationTime', order: 'descending'}"
        highlight-current-row
        size="small"
        @sort-change="onSortChange"
      >
        <el-table-column
          prop="userName"
          label="姓名"
          sortable="custom"
        />
        <el-table-column
          prop="email"
          label="邮箱地址"
          sortable="custom"
        />
        <el-table-column
          prop="phoneNumber"
          label="手机号"
          sortable="custom"
        />
        <el-table-column
          prop="creationTime"
          label="创建时间"
          sortable="custom"
        >
          <template slot-scope="scope">
            {{ scope.row.creationTime | formatDate }}
          </template>
        </el-table-column>
        <el-table-column
          label="操作"
        >
          <template slot-scope="scope">
            <el-link
              v-permission="['AbpIdentity.Users.Update']"
              type="primary"
              icon="el-icon-edit"
              :underline="false"
              @click="edit(scope.row)"
            />
            <el-link
              v-permission="['AbpIdentity.Users.ManagePermissions']"
              type="primary"
              icon="el-icon-setting"
              :underline="false"
              title="权限"
              @click="menuGrant(scope.row)"
            />
            <el-popconfirm
              v-permission="['AbpIdentity.Users.Delete']"
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
      :roles="allRoles"
      dialog-width="500px"
      @close="dialogClose"
    />
    <EditDialog
      :visible.sync="editDialogVisible"
      :user-id="editUserid"
      :close-confirm="true"
      :roles="allRoles"
      dialog-width="700px"
      @close="dialogClose"
    />
    <MenuGrant
      :visible.sync="menuGrantDialogVisible"
      :close-confirm="true"
      :provider-key="menuGrantUserId"
      provider-name="U"
      :name="menuGrantUserName"
      dialog-width="700px"
      @close="dialogClose"
    />
  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import userApi from '@/api/user'
import roleApi from '@/api/role'
import CreateDialog from './components/UserCreateDialog'
import EditDialog from './components/UserEditDialog'
import MenuGrant from '@/components/MenuGrant'
import Pagination from '@/components/Pagination'

export default {
  name: 'Index',
  components: { CreateDialog, EditDialog, MenuGrant, Pagination },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      menuGrantDialogVisible: false,
      editUserid: '',
      menuGrantUserName: '',
      menuGrantUserId: '',
      /**
       * 所有角色。
       */
      allRoles: [],

      query: {
        filter: ''
      }
    }
  },
  created() {
    this.getList()
    this.getRoleAll()
  },
  methods: {
    getRoleAll() {
      roleApi.getAll().then(res => {
        this.allRoles = res.items
      })
    },
    getList() {
      userApi.getList(this.query).then(res => {
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
    del(row) {
      userApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    },
    menuGrant(row) {
      this.menuGrantUserId = row.id
      this.menuGrantUserName = row.name
      this.menuGrantDialogVisible = true
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
      this.editUserid = null

      this.menuGrantUserId = null
      this.menuGrantUserName = ''
    }
  }
}
</script>
