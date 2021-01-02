<template>
  <el-dialog
    custom-class="custom"
    :close-on-click-modal="false"
    title="创建"
    widht="720"
    :visible.sync="dialogVisible">
    <el-form ref="form" :model="form" :rules="rules" label-width="100px" size="small">
        <el-form-item label="客户端Id" prop="clientId">
            <el-input v-model="form.clientId"></el-input>
        </el-form-item>
        <el-form-item label="客户端名称" prop="clientName">
            <el-input v-model="form.clientName"></el-input>
        </el-form-item>
        <el-form-item label="客户 Uri">
            <el-input v-model="form.clientUri"></el-input>
        </el-form-item>
        <el-form-item label="Logo Uri">
            <el-input v-model="form.logoUri"></el-input>
        </el-form-item>
        <el-form-item label="描述">
            <el-input type="textarea" v-model="form.description"></el-input>
        </el-form-item>
        <el-form-item label="客户端类型">
            <el-radio-group v-model="form.clientType" class="multiline">
                <el-radio :label="0">Empty</el-radio>
                <el-radio :label="1">Device</el-radio>
                <el-radio :label="2">WebServerSideRenderer</el-radio>
                <el-radio :label="3">Spa</el-radio>
                <el-radio :label="4">WebHybrid</el-radio>
                <el-radio :label="5">Native</el-radio>
                <el-radio :label="6">Machine</el-radio>
            </el-radio-group>
        </el-form-item>
     </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false" size="small">取 消</el-button>
        <el-button type="primary" @click="handleSubmit" size="small">保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { createClient } from '@/api/identity-server/client'
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
    data(){
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
        handleSubmit(){
            this.$refs.form.validate((valid) => {
                if(!valid) return

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
            });
        },
        showDialog(){
            this.form =  _.cloneDeep(defaultForm)
            this.dialogVisible = true
        }
    }
}
</script>

<style>

</style>