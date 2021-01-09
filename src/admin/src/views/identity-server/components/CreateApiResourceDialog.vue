<template>
  <el-dialog
    custom-class="custom"
    :close-on-click-modal="false"
    title="创建"
    width="880px"
    :visible.sync="dialogVisible">
    <el-form ref="form" :model="form" :rules="rules" label-width="100px" size="small">
        <el-form-item>
            <span slot="label">
                名称 
                <el-tooltip content="API 的唯一名称。 此值用于自我认证，并将添加到传给受众的访问令牌 access_token 中。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-input v-model="form.name"></el-input>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                显示名称 
                <el-tooltip content="该值可以用于例如 在同意屏幕上。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-input v-model="form.displayName"></el-input>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                描述 
                <el-tooltip content="该值可以用于例如 在同意屏幕上。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-input v-model="form.description" type="textarea"></el-input>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                启用 
                <el-tooltip content="指示此资源是否已启用且可以请求。 默认为 true。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-switch v-model="form.enabled"></el-switch>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                用户声明 
                <el-tooltip content="应包含在访问令牌中的关联用户声明类型的列表。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <SmartPicker type="claim" v-model="form.userClaims"></SmartPicker>
        </el-form-item>
     </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false" size="small">取 消</el-button>
        <el-button type="primary" @click="handleSubmit" size="small">保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  updateApiResource, getApiResource, createApiResource
} from '@/api/identity-server/apiResource'
import SecretEditor from './SecretEditor'
import SmartPicker from './SmartPicker'
import _ from 'lodash'

const defaultForm = {
    name: '',
    displayName: '',
    description: '',
    description: '',
    enabled: true,
    required: false,
    emphasize: false,
    showInDiscoveryDocument: true, 
    userClaims: [],
    secrets: []
}

const userClaimSuggestions = [ 'sub','name', 'given_name', 'family_name', 'middle_name', 'nickname', 
    'preferrred_username', 'profile', 'picture', 'website', 'email', 'email_verified', 'gender', 
    'birthdate', 'zoneinfo', 'locale', 'phone_number', 'phone_number_verified', 'address', 'updated_at'  ]

export default {
    components: { SecretEditor, SmartPicker },
    data(){
        return {
            form: _.cloneDeep(defaultForm),
            rules: {
                name: [
                    { required: true, message: '请输入名称', trigger: 'blur' }
                ]
            },
            dialogVisible: false,
            userClaimSuggestions
        }
    },
    computed: {
        scopeCount(){
            if(!this.form.scopes) return 0
            return this.form.scopes.length
        }
    },
    methods: {
        handleSubmit(){
            this.$refs.form.validate((valid) => {
                if(!valid) return

                createApiResource(this.form).then(res => {
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
        handleSuggestionAdd(userClaim){
            const isExist = this.form.userClaims.findIndex(e => e === userClaim) !== -1
            if(isExist) return 

            this.form.userClaims.push(userClaim)
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