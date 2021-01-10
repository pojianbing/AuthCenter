<template>
  <el-card :shadow="shadow" class="card" :class="[data.checked ? 'checked' : '']">
    <div class="card-body">
      <div class="img-wrapper">
        <svg-icon :icon-class="data.icon" style="font-size:40px" class="icon" />
      </div>
      <h3>{{ data.name }}</h3>
      <h4>{{ data.desc }}</h4>
    </div>

  </el-card>
</template>

<script>
export default {
  props: {
    data: {
      type: Object,
      isRequired: true,
      default() {
        return {}
      }
    }
  },
  computed: {
    shadow() {
      return this.data.checked ? 'always' : 'hover'
    }
  }
}
</script>

<style scoped>

  .card{
    height: 220px;
  }

  .card h4{
    color: #909399;
    font-size:13px;
  }

  .card.checked  h4,   .card.checked h3{
    color: #606266;
  }

  .card h3{
    color: #909399;
    margin-top: 50px;
  }

  .card-body{
    display: flex;
    flex-direction : column;
    align-items: center;
    justify-content: center;
  }

  .img-wrapper{
    display: flex;
    align-items: center;
    justify-content: center;
    -ms-border-radius: 15px;
    border-radius: 15px;
  }

  .checked{
    background-color: #F2F6FC;
  }

  .checked .icon{
    color: #409EFF;
  }

</style>
