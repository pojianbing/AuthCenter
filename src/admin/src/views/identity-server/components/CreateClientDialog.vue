<template>
  <el-dialog
    custom-class="custom"
    :close-on-click-modal="false"
    title="创建"
    width="880px"
    :visible.sync="dialogVisible"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="120px" size="small">
      <el-form-item label="客户端标识" prop="clientId">
        <span slot="label">
          客户端标识
          <el-tooltip content="客户端的唯一标识" placement="top">
            <i class="el-alert__icon el-icon-info" />
          </el-tooltip>
        </span>
        <el-input v-model="form.clientId" />
      </el-form-item>
      <el-form-item label="客户端名称" prop="clientName">
        <span slot="label">
          客户端名称
          <el-tooltip content="客户端显示名称（用于记录和同意屏幕）" placement="top">
            <i class="el-alert__icon el-icon-info" />
          </el-tooltip>
        </span>
        <el-input v-model="form.clientName" />
      </el-form-item>
      <ClientTypeSelector v-model="form.clientType" />
    </el-form>

    <div slot="footer" class="dialog-footer">
      <el-button size="small" @click="dialogVisible = false">取 消</el-button>
      <el-button type="primary" size="small" @click="handleSubmit">保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { createClient } from '@/api/identity-server/client'
import ClientTypeSelector from './ClientTypeSelector'
import _ from 'lodash'

const defaultForm = {
  clientId: '',
  clientName: '',
  description: '',
  clientUri: '',
  logoUri: '',
  clientType: 0
}

export default {
  components: { ClientTypeSelector },
  data() {
    return {
      form: _.cloneDeep(defaultForm),
      rules: {
        clientId: [
          { required: true, message: '请输入客户端Id', trigger: 'blur' }
        ],
        clientName: [
          { required: true, message: '请输入客户端名称', trigger: 'blur' }
        ]
      },
      dialogVisible: false
    }
  },
  methods: {
    handleSubmit() {
      this.$refs.form.validate((valid) => {
        if (!valid) return

        createClient(this.form).then(res => {
          this.$notify({
            title: '成功',
            message: '操作成功',
            type: 'success',
            duration: 2000
          })
          this.dialogVisible = false
          this.$emit('success')
        })
      })
    },
    showDialog() {
      this.form = _.cloneDeep(defaultForm)
      this.dialogVisible = true
    }
  }
}
</script>

<style>

</style>
