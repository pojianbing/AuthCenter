<template>
  <el-card shadow="never" class="picker">
    <!-- 输入框 -->
    <el-input v-model="searchText" placeholder="输入2个以上字符" @input="handleSearch" />

    <!-- 搜索结果 -->
    <template v-if="searchItems.length !== 0">
      <el-divider />
      <div class="search-title">搜索结果:(点击选择)</div>
      <div class="search-result">
        <el-button v-for="(item, index) in searchItems" :key="index" type="primary" @click="handleSelectSearchItem(item)">{{ item }}</el-button>
      </div>
    </template>
    <vue-loaders-line-scale v-if="loading" color="#3cb371" scale="0.5" />

    <!-- 选中项 -->
    <el-divider />
    <template v-if="selectedItems.length === 0">
      <div class="search-title">没有选中项</div>
    </template>
    <template v-else>
      <div class="search-title">选中项:</div>
      <div class="selected-result">
        <div v-for="(item, index) in selectedItems" :key="index">
          <el-button type="primary" plain>{{ item }}</el-button>
          <el-button type="success" icon="el-icon-edit" circle plain @click="handleUpdateSelectedItem(item, index)" />
          <el-button type="danger" icon="el-icon-delete" circle plain @click="handleDeleteSelectedItem(index)" />
        </div>
      </div>
    </template>

    <!-- 建议项 -->
    <template v-if="suggestItems.length !== 0">
      <el-divider />
      <div class="search-title">建议项:</div>
      <div class="suggest-result">
        <el-button v-for="(item, index) in suggestItems" :key="index" class="suggestion" @click="handleSelectSuggetItem(item)">{{ item }}</el-button>
        <el-button v-if="!loadMore" icon="el-icon-plus" @click="handleLoadMoreSugget">更多</el-button>
      </div>
    </template>
  </el-card>
</template>

<script>
import { searchConsts } from '@/api/identity-server/client'
import { debounce } from '@/utils'

export default {
  props: {
    type: {
      type: String,
      isRequired: false,
      default() {
        return ''
      }
    },
    value: {
      type: Array,
      isRequired: true,
      default() {
        return []
      }
    }
  },
  data() {
    return {
      searchItems: [],
      selectedItems: [],
      suggestItems: [],
      searchText: '',
      loading: false,
      loadMore: false
    }
  },
  computed: {
    // 是否需要调用远程接口
    isRemote() {
      return !!this.type
    }
  },
  watch: {
    value: {
      handler(newval, old) {
        newval = newval || []
        this.selectedItems = [...newval]
      },
      immediate: true
    }
  },
  mounted() {
    if (this.isRemote) {
      this.loadSuggest()
    }
  },
  created() {
    this.$_searchHandler = debounce(() => {
      // 输入为空，清空搜索结果
      if (!this.searchText) {
        this.clearSearchResult()
        return
      }
      // 2个字符以上，则搜索
      if (this.searchText.length >= 2) {
        this.loading = true
        this.search(this.searchText).then(res => {
          this.searchItems = res
        }).finally(() => {
          this.loading = false
        })
      }
    }, 600)
  },
  methods: {
    handleSearch() {
      this.$_searchHandler()
    },
    handleSelectSearchItem(item) {
      this.trySelectItem(item)
      this.clearSearchResult()
      this.clearSearchText()
    },
    handleSelectSuggetItem(item) {
      this.trySelectItem(item)
    },
    handleLoadMoreSugget() {
      this.loadMoreSugget()
    },
    handleDeleteSelectedItem(index) {
      this.selectedItems.splice(index, 1)
      this.$emit('input', [...this.selectedItems])
    },
    handleUpdateSelectedItem(item, index) {
      var self = this

      this.$prompt('', item, {
        confirmButtonText: '确定',
        cancelButtonText: '取消'
      }).then(({ value }) => {
        if (self.existInSelectedItems(value, index)) {
          this.$message({
            message: `${value}已存在`,
            type: 'error'
          })
          return
        }

        this.$set(this.selectedItems, index, value)
        this.$emit('input', [...this.selectedItems])
      }).catch(() => {})
    },
    search(searchText) {
      if (this.isRemote) {
        return new Promise((resolve, reject) => {
          searchConsts(this.type, searchText).then(res => {
            var exist = res.some(e => e === searchText)
            if (exist) {
              resolve([...res])
            } else {
              resolve([...res, searchText])
            }
          })
        })
      } else {
        return new Promise((resolve, reject) => {
          resolve([searchText])
        })
      }
    },
    trySelectItem(item) {
      if (this.existInSelectedItems(item, -1)) {
        this.$message({
          message: `${item} 项已被选中`,
          type: 'error'
        })
        return
      }

      this.selectedItems.push(item)
      this.$emit('input', [...this.selectedItems])
    },
    clearSearchResult() {
      this.searchItems = []
    },
    clearSearchText() {
      this.searchText = ''
    },
    existInSelectedItems(item, index) {
      return this.selectedItems.some((e, idx) => idx !== index && e === item)
    },
    loadSuggest() {
      searchConsts(this.type, '', 5).then(res => {
        this.suggestItems = res
      })
    },
    loadMoreSugget() {
      this.loadMore = true
      searchConsts(this.type, '').then(res => {
        this.suggestItems = res
      })
    }
  }
}
</script>

<style scoped>

  .picker .search-title {
    color: gray;
    font-size: 12px;
  }

  .picker .el-divider{
    margin-bottom: 0;
  }

  .picker .search-result .el-button{
    margin-top: 5px;
  }

  .picker .selected-result>div{
    margin-top: 5px;
  }

  .picker .suggest-result .suggestion{
    border: none;
  }

</style>
