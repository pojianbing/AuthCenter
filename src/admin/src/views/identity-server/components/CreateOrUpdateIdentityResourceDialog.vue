<template>
  <el-dialog
    custom-class="custom"
    :close-on-click-modal="false"
    title="创建"
    width="880px"
    :visible.sync="dialogVisible">
    <el-form ref="form" :model="form" :rules="rules" label-width="100px" size="small">
        <el-form-item label="名称" prop="name">
            <el-input v-model="form.name"></el-input>
        </el-form-item>
        <el-form-item label="显示名称" prop="displayName">
            <el-input v-model="form.displayName"></el-input>
        </el-form-item>
        <el-form-item label="描述">
            <el-input v-model="form.description"></el-input>
        </el-form-item>
        <el-form-item label="启用">
            <el-checkbox v-model="form.enabled" label="是"></el-checkbox>
        </el-form-item>
        <el-form-item label="必要">
            <el-checkbox v-model="form.required" label="是"></el-checkbox>
        </el-form-item>
        <el-form-item label="强调">
            <el-checkbox v-model="form.emphasize" label="是"></el-checkbox>
        </el-form-item>
        <el-form-item label="在发现文档中显示">
            <el-checkbox v-model="form.showInDiscoveryDocument" label="是"></el-checkbox>
        </el-form-item>
        <el-form-item label="声明">
            <el-select style="width: 100%"
                v-model="form.userClaims"
                multiple
                filterable
                allow-create
                default-first-option
                placeholder="Add Grant">
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

const userClaimSuggestions = [ 'sub','name', 'given_name', 'family_name', 'middle_name', 'nickname', 
    'preferrred_username', 'profile', 'picture', 'website', 'email', 'email_verified', 'gender', 
    'birthdate', 'zoneinfo', 'locale', 'phone_number', 'phone_number_verified', 'address', 'updated_at'  ]

export default {
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