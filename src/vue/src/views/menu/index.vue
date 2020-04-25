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
        v-model="query.name"
        placeholder="名称"
        clearable
        class="header-item"
        size="mini"
      />
      <el-button class="header-item-btn" type="primary" size="mini" @click="getList">搜索</el-button>
      <el-button
        v-permission="['MenuManagement.Menus.Create']"
        class="header-item-btn"
        type="success"
        size="mini"
        @click="create"
      >新增</el-button>
    </div>
    <div class="app-full-body">
      <el-table
        :data="tableData"
        :expand-row-keys="expandKeys"
        highlight-current-row
        row-key="id"
        size="small"
        @sort-change="onSortChange"
      >
        <el-table-column
          prop="displayName"
          label="名称"
          min-width="300"
          fixed="left"
          show-overflow-tooltip
        />
        <el-table-column
          prop="icon"
          label="图标"
          width="80"
          show-overflow-tooltip
        />
        <el-table-column
          prop="menuType"
          label="类型"
          width="100"
          show-overflow-tooltip
        >
          <template slot-scope="scope">
            <el-tag v-if="scope.row.menuType === 0" size="small" type="primary">菜单</el-tag>
            <el-tag v-if="scope.row.menuType === 1" size="small" type="success">权限</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="routerPath"
          label="路由地址"
          min-width="200"
          show-overflow-tooltip
        />
        <el-table-column
          prop="componentPath"
          label="Vue组件"
          min-width="200"
          show-overflow-tooltip
        />
        <el-table-column
          prop="permissionKey"
          label="权限"
          min-width="200"
          show-overflow-tooltip
        />
        <el-table-column
          prop="sort"
          label="排序"
          width="50"
          show-overflow-tooltip
        />
        <el-table-column
          prop="creationTime"
          label="创建时间"
          width="150"
          show-overflow-tooltip
        >
          <template slot-scope="scope">
            {{ scope.row.creationTime | formatDate }}
          </template>
        </el-table-column>
        <el-table-column
          prop="lastModificationTime"
          label="更新时间"
          width="150"
        >
          <template slot-scope="scope">
            {{ scope.row.lastModificationTime | formatDate }}
          </template>
        </el-table-column>

        <el-table-column
          label="操作"
          width="150"
          fixed="right"
        >
          <template slot-scope="scope">
            <el-link
              v-if="scope.row.menuType == 0"
              v-permission="['MenuManagement.Menus.CreatePermission']"
              type="primary"
              icon="el-icon-plus"
              title="添加权限"
              :underline="false"
              @click="addPermission(scope.row)"
            />
            <el-link
              v-permission="['MenuManagement.Menus.Update']"
              type="primary"
              icon="el-icon-edit"
              :underline="false"
              @click="edit(scope.row)"
            />
            <el-popconfirm
              v-permission="['MenuManagement.Menus.Delete']"
              placement="top"
              title="确定删除此项？"
              @onConfirm="del(scope.row)"
            >
              <el-link slot="reference" type="danger" icon="el-icon-delete" :underline="false" />
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
      dialog-width="700px"
      @close="dialogClose"
    />
    <EditDialog
      :visible.sync="editDialogVisible"
      :menu-id="editMenuId"
      :close-confirm="true"
      dialog-width="700px"
      @close="dialogClose"
    />
    <PermissionCreateDialog
      :visible.sync="createPermissionDialogVisible"
      :close-confirm="true"
      :parent-menu="permissionParentMenu"
      dialog-width="500px"
      @close="dialogClose"
    />
    <PermissionEditDialog
      :visible.sync="editPermissionDialogVisible"
      :menu-id="editPermissionId"
      :parent-menu="permissionParentMenu"
      :close-confirm="true"
      dialog-width="500px"
      @close="dialogClose"
    />

  </div>
</template>

<script>
import listMixin from '@/mixins/listMixin'
import menuApi from '@/api/menu'
import Pagination from '@/components/Pagination'
import CreateDialog from './components/MenuCreateDialog'
import EditDialog from './components/MenuEditDialog'
import PermissionCreateDialog from './components/PermissionCreateDialog'
import PermissionEditDialog from './components/PermissionEditDialog'

export default {
  name: 'Index',
  components: {
    CreateDialog,
    EditDialog,
    PermissionCreateDialog,
    PermissionEditDialog,
    Pagination
  },
  mixins: [listMixin],
  data() {
    return {
      createDialogVisible: false,
      editDialogVisible: false,
      createPermissionDialogVisible: false,
      editPermissionDialogVisible: false,
      editMenuId: '',
      permissionParentMenu: null,
      editPermissionId: '',
      expandKeys: [],
      query: {
        name: ''
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      menuApi.getList({
        name: this.query.name
      }).then(res => {
        this.tableData = res.items
        this.updateTotalCount(res.totalCount)

        // 展开第一级
        res.items.forEach(x => {
          this.expandKeys.push(x.id)
        })
      })
    },
    dialogClose(refresh) {
      if (refresh) {
        this.getList()
      }
      this.editMenuId = null
    },
    create() {
      this.createDialogVisible = true
    },
    edit(row) {
      if (row.menuType === 0) {
        this.editMenuId = row.id
        this.editDialogVisible = true
      } else if (row.menuType === 1) {
        this.editPermissionId = row.id
        this.editPermissionDialogVisible = true
      }
    },
    del(row) {
      menuApi.delete(row.id).then(() => {
        this.$message('删除成功')
        this.getList()
      })
    },
    addPermission(row) {
      this.permissionParentMenu = row
      this.createPermissionDialogVisible = true
    }
  }
}
</script>
