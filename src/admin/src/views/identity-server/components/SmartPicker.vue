<template>
  <el-card shadow="never" class="picker">
      <!-- 输入框 -->
      <el-input placeholder="输入2个以上字符" v-model="searchText" @input="handleSearch"></el-input>

      <!-- 搜索结果 -->
      <template v-if="searchItems.length !== 0">
        <el-divider></el-divider>
        <div class="search-title">搜索结果:(点击选择)</div>
        <div class="search-result">
          <el-button type="primary" v-for="(item, index) in searchItems" :key="index" @click="handleSelectSearchItem(item)">{{ item }}</el-button>
        </div>
      </template>
      <div class="loadingArea">
aa
      </div>

      <!-- 选中项 -->
      <el-divider></el-divider>
      <template v-if="selectedItems.length === 0">
        <div class="search-title">没有选中项</div>
      </template>
      <template v-else>
        <div class="search-title">选中项:</div>
        <div class="selected-result">
          <div v-for="(item, index) in selectedItems" :key="index">
            <el-button type="primary" plain>{{ item }}</el-button>
            <el-button type="success" icon="el-icon-edit" circle plain @click="handleUpdateSelectedItem(item, index)"></el-button>
            <el-button type="danger" icon="el-icon-delete" circle plain @click="handleDeleteSelectedItem(index)"></el-button>
          </div>
        </div>
      </template>

      <!-- 建议项 -->
      <template v-if="suggestItems.length !== 0">
        <el-divider></el-divider>
        <div class="search-title">建议项:</div>
        <div class="suggest-result">
          <el-button v-for="(item, index) in suggestItems" :key="index">{{ item }}</el-button>
        </div>
      </template>
  </el-card>
</template>

<script>
import { searchConsts } from '@/api/identity-server/client'
import { debounce } from '@/utils'

export default {
  props:{
    type: {
      type: String,
      isRequired: false,
      default(){
        return ''
      },
      searchingLoading: true
    }
  },
  data(){
    return {
      searchItems: [],
      selectedItems: [],
      suggestItems: [],
      searchText: ''
    }
  },
  computed: {
    // 是否需要调用远程接口
    isRemote(){
      return !!this.type
    }
  },
  mounted() {
    searchConsts('scope', 'code', 1)
    searchConsts('claim', 'code', 1)
    searchConsts('grantType', 'code', 1)
  },
  created() {
    this.$_searchHandler = debounce(() => {
      // 输入为空，清空搜索结果
      if(!this.searchText) {
        this.clearSearchResult()
        return
      }
      // 2个字符以上，则搜索
      if(this.searchText.length >= 2){
        this.$loading({
          text: 'Loading',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)',
          target: 'loadingArea'
        })

        this.search(this.searchText).then(res => {
          this.searchItems = res
        })
      }
    }, 600)
  },
  methods: {
    handleSearch(){
      this.$_searchHandler()
    },
    handleSelectSearchItem(item){
      if(this.existInSelectedItems(item, -1)){
        this.$message({
          message: `${item} 项已被选中`,
          type: 'error'
        })
        return
      }

      this.selectedItems.push(item)
      this.clearSearchResult()
      this.clearSearchText()
    },
    handleDeleteSelectedItem(index){
      this.selectedItems.splice(index,1)
    },
    handleUpdateSelectedItem(item, index){
      var self = this

       this.$prompt('', item, {
          confirmButtonText: '确定',
          cancelButtonText: '取消'
        }).then(({ value }) => {
          if(self.existInSelectedItems(value, index)) {
            this.$message({
              message: `${value}已存在`,
              type: 'error'
            })
            return
          }

          this.$set(this.selectedItems, index, value)
        }).catch(() => {});
    },
    search(searchText){
      if(this.isRemote){
        return new Promise((resolve, reject) => {
          searchConsts(this.type, searchText).then(res => {
            var exist = res.some(e=> e === searchText)
            if(exist){
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
    clearSearchResult(){
      this.searchItems = []
    },
    clearSearchText(){
      this.searchText = ''
    },
    existInSelectedItems(item, index){
      return this.selectedItems.some((e, idx) => idx !== index && e === item)
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

  .picker .suggest-result .el-button{
    border: none;
  }

  .loadingArea{
    width: 100%;height: 30px;
  }

</style>