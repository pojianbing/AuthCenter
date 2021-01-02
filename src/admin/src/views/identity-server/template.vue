<template>
  <el-card shadow="never">
      <div class="side-table-container">
          <div class="table-area full">
              <div class="action-filter">
                <div class="actions">
                    <el-button type="primary" size="small">功能按钮</el-button>
                </div>
                <div style="margin-left:50px" class="filters">
                    <el-input
                        v-model="queryForm.keywords"
                        size="small"
                        style="margin-right:5px"
                        placeholder="请输入关键字搜索"
                        prefix-icon="el-icon-search"
                        />
                    <el-button size="small" @click="handleSearch">搜索</el-button>
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
                <el-table-column type="selection" width="40" align="center" />
                <el-table-column
                    label="客户端id"
                    prop="clientId"
                    align="center"
                >
                    <template slot-scope="{ row }">
                    <span>{{ row.clientId | empty }}</span>
                    </template>
                </el-table-column>
                <el-table-column
                    label="客户端名称"
                    prop="clientName"
                    align="center"
                >
                    <template slot-scope="{ row }">
                    <span>{{ row.clientName | empty }}</span>
                    </template>
                </el-table-column>
                <el-table-column
                    label="操作"
                    prop="action"
                    align="center"
                    width="160"
                >
                    <template slot-scope="{ row }">
                        <el-dropdown type="primary" size="mini">
                            <el-button type="primary" size="mini">
                                操作<i class="el-icon-arrow-down el-icon--right"></i>
                            </el-button>
                            <el-dropdown-menu slot="dropdown">
                                <el-dropdown-item>黄金糕</el-dropdown-item>
                                <el-dropdown-item>狮子头</el-dropdown-item>
                                <el-dropdown-item>螺蛳粉</el-dropdown-item>
                                <el-dropdown-item>双皮奶</el-dropdown-item>
                                <el-dropdown-item>蚵仔煎</el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </template>
                </el-table-column>
              </el-table>
              <div class="bottom">
                <div>
                    已选{{ seletedDatas.length }}条
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
  getClients
} from '@/api/identity-server/client'
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
      seletedDatas: [],
    }
  },
  created() {
    this.getList()
  },
  methods: {
    handleSelectionChange(rows) {
      this.seletedDatas = rows
    },
    handleSearch(){
      this.getList()
    },
    getList() {
      this.listLoading = true
      getClients(this.queryForm).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
  }
}
</script>

<style>

</style>