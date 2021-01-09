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
                <el-tooltip content="身份资源的唯一名称。 这是客户端将用于授权请求中的作用域参数的值。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-input v-model="form.name"></el-input>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                显示名称 
                <el-tooltip content="该值将用于例如 在同意屏幕上。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-input v-model="form.displayName"></el-input>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                描述 
                <el-tooltip content="该值将用于例如 在同意屏幕上。" placement="top">
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
                在发现文档中显示 
                <el-tooltip content="指定此作用域是否显示在发现文档中。 默认为 true。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-switch v-model="form.showInDiscoveryDocument"></el-switch>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                必须 
                <el-tooltip content="指定用户是否可以在同意屏幕上取消选择作用域（如果同意屏幕要实现此类功能）。 默认为 false。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-switch v-model="form.required"></el-switch>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                强调 
                <el-tooltip content="指定同意屏幕是否会强调此作用域（如果同意屏幕要实现此类功能）。 将此设置用于敏感或重要作用域。 默认为 false。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <el-switch v-model="form.emphasize"></el-switch>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                用户声明 
                <el-tooltip content="应包含在身份令牌 id_token 中的关联用户声明类型的列表。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <SmartPicker type="claim" v-model="form.userClaims"></SmartPicker>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                属性
                <el-tooltip content="字典可根据需要保存任何自定义客户端特定值。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <PropertyEditor v-model="form.properties"></PropertyEditor>
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
  updateIdentityResource, getIdentityResource, createIdentityResource
} from '@/api/identity-server/identityResource'
import ClaimsEditor from './ClaimsEditor'
import PropertyEditor from './PropertyEditor'
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
    userClaims: []
}

export default {
    components: { ClaimsEditor, PropertyEditor, SmartPicker },
    data(){
        return {
            form: _.cloneDeep(defaultForm),
            rules: {
                name: [
                    { required: true, message: '请输入名称', trigger: 'blur' }
                ]
            },
            dialogVisible: false
        }
    },
    methods: {
        handleSubmit(){
            this.$refs.form.validate((valid) => {
                if(!valid) return

                if(this.form.id){
                    updateIdentityResource(this.form).then(res => {
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
                    createIdentityResource(this.form).then(res => {
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
        showDialog(id){
            if(id){
                getIdentityResource(id).then(res => {
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