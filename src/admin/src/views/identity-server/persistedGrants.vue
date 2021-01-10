<template>
  <el-card shadow="never">
    <div class="side-table-container">
      <div class="table-area full">
        <div class="action-filter">
          <div class="actions" />
          <div style="margin-left:50px" class="filters">
            <el-input
              v-model="queryForm.keywords"
              size="small"
              style="margin-right:5px"
              :placeholder="$t('AbpIdentityServer[\'SearchHint\']')"
              prefix-icon="el-icon-search"
            />
            <el-button size="small" @click="handleSearch">{{ $t("AbpIdentityServer['Search']") }}</el-button>
          </div>
        </div>
        <el-table
          :key="tableKey"
          v-loading="listLoading"
          :data="list"
          border
          fit
          highlight-current-row
          size="small"
          style="width: 100%;"
          @selection-change="handleSelectionChange"
        >
          <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" inline class="demo-table-expand">
                <el-form-item label="数据">
                  <span>{{ props.row.data }}</span>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <el-table-column
            :label="$t('AbpIdentityServer[\'SubjectId\']')"
            prop="subjectId"
            align="center"
            width="150"
          >
            <template slot-scope="{ row }">
              <span>{{ row.subjectId | empty }}</span>
            </template>
          </el-table-column>
          <el-table-column
            :label="$t('AbpIdentityServer[\'Type\']')"
            prop="type"
            align="center"
            width="250"
          >
            <template slot-scope="{ row }">
              <span>{{ row.type | empty }}</span>
            </template>
          </el-table-column>
          <el-table-column
            :label="$t('AbpIdentityServer[\'Expiration\']')"
            prop="expiration"
            align="center"
          >
            <template slot-scope="{ row }">
              <span v-if="row.expiration">{{ row.expiration | moment('yyyy-MM-DD HH:mm:ss') }}</span>
              <span v-else>--</span>
            </template>
          </el-table-column>
          <el-table-column
            :label="$t('AbpIdentityServer[\'Operation\']')"
            prop="action"
            align="center"
            width="220"
          >
            <template slot-scope="{ row }">
              <el-button
                size="mini"
                type="danger"
                icon="el-icon-delete"
                @click="handleDelete(row.id)"
              >
                {{ $t("AbpIdentityServer['Delete']") }}
              </el-button>
            </template>
          </el-table-column>
        </el-table>
        <div class="bottom">
          <div>
            <!-- 已选{{ seletedDatas.length }}条 -->
          </div>
          <pagination
            v-show="total > 0"
            :total="total"
            :page.sync="queryForm.page"
            :limit.sync="queryForm.limit"
            @pagination="getList"
          />
        </div>
      </div>
    </div>
  </el-card>
</template>

<script>
import {
  getPersistedGrants, deletePersistedGrant
} from '@/api/identity-server/persistedGrant'
import baseListQuery from '@/utils/abp'
import Pagination from '@/components/Pagination'

export default {
  components: { Pagination },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      queryForm: Object.assign({
        keywords: undefined
      }, baseListQuery),
      seletedDatas: []
    }
  },
  created() {
    this.getList()
  },
  methods: {
    handleSelectionChange(rows) {
      this.seletedDatas = rows
    },
    handleSearch() {
      this.getList()
    },
    handleDelete(id) {
      this.$confirm('确定要删除？', '确定', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deletePersistedGrant(id).then(res => {
          this.$notify({
            title: '成功',
            message: '操作成功',
            type: 'success',
            duration: 2000
          })
          this.getList()
        })
      }).catch(() => {})
    },
    handleCreateOrUpdateSuccess() {
      this.getList()
    },
    getList() {
      this.listLoading = true
      getPersistedGrants(this.queryForm).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    }
  }
}
</script>

<style>

</style>
