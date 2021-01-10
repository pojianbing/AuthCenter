<template>
  <el-card shadow="never">
    <el-row align="middle" justify="center" type="flex">
      <el-col :span="3" class="fieldName">键</el-col>
      <el-col :span="7" :offset="1" class="fieldValue">
        <el-input v-model="clientProperty.key" />
      </el-col>
      <el-col :span="3" class="fieldName">值</el-col>
      <el-col :span="7" :offset="1" class="fieldValue">
        <el-input v-model="clientProperty.value" />
      </el-col>
    </el-row>
    <el-row justify="end" type="flex">
      <el-button type="primary" @click="handleAdd">添加</el-button>
      <el-col :span="1" />
    </el-row>

    <el-table :data="clientPropertys" style="width: 100%;margin-top:20px" border>
      <el-table-column prop="key" label="类型" />
      <el-table-column prop="value" label="值" />
      <el-table-column
        fixed="right"
        label="操作"
        width="100"
      >
        <template slot-scope="{ row, $index }">
          <el-button type="text" size="mini" @click="handleDelete(row, $index)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </el-card>
</template>

<script>
import _ from 'lodash'

const defaultClientProperty = {
  key: '',
  value: ''
}

export default {
  props: {
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
      clientProperty: _.cloneDeep(defaultClientProperty),
      clientPropertys: []
    }
  },
  watch: {
    value: {
      handler(val) {
        this.clientPropertys = _.cloneDeep(val || [])
      },
      immediate: true
    }
  },
  methods: {
    handleAdd() {
      this.clientPropertys.push(this.clientProperty)
      this.clientProperty = _.cloneDeep(defaultClientProperty)
      this.onModelChange()
    },
    handleDelete(row, index) {
      this.clientPropertys.splice(index, 1)
      this.onModelChange()
    },
    onModelChange() {
      this.$emit('input', _.cloneDeep(this.clientPropertys))
    }
  }
}
</script>

<style scoped>

    .el-row{
        margin-top:15px;
    }

    .fieldName{
        text-align: right;
    }

</style>
