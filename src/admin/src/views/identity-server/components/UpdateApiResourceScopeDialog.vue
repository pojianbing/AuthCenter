<template>
  <el-dialog
    custom-class="custom"
    :close-on-click-modal="false"
    title="管理Api作用域"
    width="880px"
    :append-to-body="true"
    :visible.sync="dialogVisible"
  >
    <el-card shadow="never" title="Api 作用域">
      <el-form ref="form" :model="form" :rules="rules" label-width="100px" size="small">
        <el-form-item prop="name">
          <span slot="label">
            名称
            <el-tooltip content="AAPI 的唯一名称。 此值用于自我认证，并将添加到传给受众的访问令牌 access_token 中。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <el-input v-model="form.name" />
        </el-form-item>
        <el-form-item>
          <span slot="label">
            显示名称
            <el-tooltip content="该值可以用于例如 在同意屏幕上。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <el-input v-model="form.displayName" />
        </el-form-item>
        <el-form-item>
          <span slot="label">
            描述
            <el-tooltip content="该值可以用于例如 在同意屏幕上。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <el-input v-model="form.description" type="textarea" />
        </el-form-item>
        <el-form-item>
          <span slot="label">
            必须
            <el-tooltip content="指定用户是否可以在同意屏幕上取消选择作用域（如果同意屏幕要实现此类功能）。 默认为 false。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <el-switch v-model="form.required" />
        </el-form-item>
        <el-form-item>
          <span slot="label">
            强调
            <el-tooltip content="指定同意屏幕是否会强调此作用域（如果同意屏幕要实现此类功能）。 将此设置用于敏感或重要作用域。 默认为 false。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <el-switch v-model="form.emphasize" />
        </el-form-item>
        <el-form-item>
          <span slot="label">
            用户声明
            <el-tooltip content="应包含在身份令牌 id_token 中的关联用户声明类型的列表。" placement="top">
              <i class="el-alert__icon el-icon-info" />
            </el-tooltip>
          </span>
          <SmartPicker v-model="form.userClaims" type="claim" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" size="small" @click="handleAdd">{{ updateIndex === -1 ? '保 存' : '更 新' }}</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card shadow="never" title="Api 作用域">
      <el-table :data="scopes" style="width: 100%;margin-top:20px" border>
        <el-table-column prop="name" label="名称" />
        <el-table-column prop="displayName" label="显示名" />
        <el-table-column
          fixed="right"
          label="操作"
          width="150"
        >
          <template slot-scope="{ row, $index }">
            <el-button type="primary" size="mini" @click="handleUpdate(row, $index)">编辑</el-button>
            <el-button type="danger" size="mini" @click="handleDelete(row, $index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <div slot="footer" class="dialog-footer">
      <el-button type="primary" size="small" @click="handleConfirm">关 闭</el-button>
    </div>
  </el-dialog>
</template>

<script>
import SmartPicker from './SmartPicker'
import { getApiResourceScope, addApiResourceScope, deleteApiResourceScope, updateApiResourceScope } from '@/api/identity-server/apiResource'
import _ from 'lodash'

const defaultForm = {
  userClaims: []
}

export default {
  components: { SmartPicker },
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
      form: _.cloneDeep(defaultForm),
      rules: {
        name: [
          { required: true, message: '请输入名称', trigger: 'blur' }
        ]
      },
      dialogVisible: false,
      scopes: [],
      updateIndex: -1,
      beforeScope: undefined
    }
  },
  watch: {
    value: {
      handler(val) {
        this.scopes = _.cloneDeep(val || [])
      },
      immediate: true
    }
  },
  methods: {
    handleAdd() {
      this.$refs.form.validate((valid) => {
        if (!valid) return

        if (this.updateIndex === -1) {
          addApiResourceScope(this.form).then(res => {
            this.notifySuccess()
            this.scopes.push(_.cloneDeep(this.form))
            this.resetForm()
          })
        } else {
          updateApiResourceScope(this.beforeScope.apiResourceId, this.beforeScope.name, this.form).then(res => {
            this.notifySuccess()
            this.$set(this.scopes, this.updateIndex, this.form)
            this.resetForm()
          })
        }
      })
    },
    handleDelete(item, index) {
      deleteApiResourceScope(item.apiResourceId, item.name).then(res => {
        this.notifySuccess()
        this.scopes.splice(index, 1)
      })
    },
    handleUpdate(item, index) {
      this.updateIndex = index
      this.form = _.cloneDeep(this.scopes[index])
      this.beforeScope = _.cloneDeep(this.form)
    },
    handleConfirm() {
      this.$emit('input', _.cloneDeep(this.scopes))
      this.dialogVisible = false
    },
    showDialog(id) {
      defaultForm.apiResourceId = id
      this.form = _.cloneDeep(defaultForm)
      getApiResourceScope(id).then(res => {
        this.scopes = res
      })
      this.dialogVisible = true
    },
    resetForm() {
      this.form = _.cloneDeep(defaultForm)
      this.updateIndex = -1
      this.beforeScope = undefined
    },
    notifySuccess() {
      this.$notify({
        title: '成功',
        message: '操作成功',
        type: 'success',
        duration: 2000
      })
    }
  }
}
</script>

<style>

</style>
