<template>
  <el-card shadow="never" class="picker">
      <!-- 输入框 -->
      <el-input placeholder="输入2个以上字符"></el-input>

      <!-- 搜索结果 -->
      <el-divider></el-divider>
      <div class="search-title">搜索结果:(点击选择)</div>
      <div class="search-result">
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
      </div>

      <!-- 选中项 -->
      <el-divider></el-divider>
      <div class="search-title">选中项:</div>
      <div class="selected-result">
        <div>
          <el-button type="primary" plain>code</el-button>
          <el-button type="success" icon="el-icon-edit" circle plain></el-button>
          <el-button type="danger" icon="el-icon-delete" circle plain></el-button>
        </div>
        <div>
          <el-button type="primary" plain>code</el-button>
          <el-button type="success" icon="el-icon-edit" circle plain></el-button>
          <el-button type="danger" icon="el-icon-delete" circle plain></el-button>
        </div>
      </div>

      <!-- 建议项 -->
      <el-divider></el-divider>
      <div class="search-title">建议项:</div>
      <div class="suggest-result">
        <el-button>主要按钮</el-button>
      </div>

  </el-card>
</template>

<script>

const config = {
  'default': { remote: false, type: 1 },
}

export default {
  data(){
    return {
      data: {
        selected: [],
        options: []
      }
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

</style>