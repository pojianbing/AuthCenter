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
                作用域 
                <el-tooltip content="API 必须至少有一个作用域。 每个作用域可以有不同的设置。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-button @click="handleManageScope" type="primary">管理作用域（{{scopeCount}}）</el-button>
        </el-form-item>
        <el-form-item label="声明">
            <el-select style="width: 100%"
                v-model="form.userClaims"
                multiple
                filterable
                allow-create
                default-first-option>
                <el-option
                    v-for="item in form.userClaims"
                    :key="item"
                    :label="item"
                    :value="item">
                </el-option>
            </el-select>
            <el-tag 
                type="info" 
                style="margin-right:5px;cursor: pointer;" 
                v-for="(item, idx) in userClaimSuggestions" 
                :key="idx" 
                @click="handleSuggestionAdd(item)">
                {{item}} <i class="el-icon-circle-plus-outline"/>
            </el-tag>
        </el-form-item>
        <el-form-item label="Secrets">
            <SecretEditor v-model="form.secrets"></SecretEditor>
        </el-form-item>
     </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false" size="small">取 消</el-button>
        <el-button type="primary" @click="handleSubmit" size="small">保 存</el-button>
    </div>

    <UpdateApiResourceScopeDialog ref="updateApiResourceScopeDialog" v-model="form.scopes"></UpdateApiResourceScopeDialog>
  </el-dialog>
</template>

<script>
import {
  updateApiResource, getApiResource, createApiResource
} from '@/api/identity-server/apiResource'
import SecretEditor from './SecretEditor'
import UpdateApiResourceScopeDialog from './UpdateApiResourceScopeDialog'
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
    components: { SecretEditor, UpdateApiResourceScopeDialog },
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

                if(this.form.id){
                    updateApiResource(this.form).then(res => {
                        this.$notify({
                            title: '成功',
                            message: '操作成功',
                            type: 'success',
                            duration: 2000
                        })
                        this.dialogVisible = false
                        this.$emit('success')
                    })
                } else {
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
                }
                
            });
        },
        handleSuggestionAdd(userClaim){
            const isExist = this.form.userClaims.findIndex(e => e === userClaim) !== -1
            if(isExist) return 

            this.form.userClaims.push(userClaim)
        },
        handleManageScope(){
            this.$refs.updateApiResourceScopeDialog.showDialog()
        },
        showDialog(id){
            if(id){
                getApiResource(id).then(res => {
                    this.form = res
                    this.dialogVisible = true
                })
            } else {
                this.form =  _.cloneDeep(defaultForm)
                this.dialogVisible = true
            }
        }
    }
}
</script>

<style>

</style>