<template>
    <el-card shadow="never">
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
            <el-button @click="handleManageScope" type="primary">管理API作用域（{{scopeCount}}）</el-button>
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
        <el-form-item>
            <span slot="label">
                密钥
                <el-tooltip content="API 密钥用于内省端点。 API 可以使用 API 名称和密钥进行内省验证。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <SecretEditor v-model="form.secrets"></SecretEditor>
        </el-form-item>
        <el-form-item>
            <span slot="label">
                属性
                <el-tooltip content="需要保留任何自定义 Api 资源特定值的字典。" placement="top">
                    <i class="el-alert__icon el-icon-info"></i>
                </el-tooltip>
            </span>
            <PropertyEditor v-model="form.properties"></PropertyEditor>
        </el-form-item>
     </el-form>
    <div class="action-bar">
        <el-button size="small" @click="handleCancel">返 回</el-button>
        <el-button type="primary" size="small" @click="handleSubmit">保 存</el-button>
    </div>
    <UpdateApiResourceScopeDialog ref="updateApiResourceScopeDialog" v-model="form.scopes"></UpdateApiResourceScopeDialog>
  </el-card>
</template>

<script>
import {
  updateApiResource, getApiResource, createApiResource
} from '@/api/identity-server/apiResource'
import SmartPicker from './components/SmartPicker'
import SecretEditor from './components/SecretEditor'
import PropertyEditor from './components/PropertyEditor'
import UpdateApiResourceScopeDialog from './components/UpdateApiResourceScopeDialog'
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
    secrets: [],
    properties: []
}

export default {
    components: { SecretEditor, SmartPicker, PropertyEditor, UpdateApiResourceScopeDialog },
    data(){
        return {
            form: _.cloneDeep(defaultForm),
            rules: {
                name: [
                    { required: true, message: '请输入名称', trigger: 'blur' }
                ]
            }
        }
    },
    mounted() {
        getApiResource(this.$route.query.id).then(res => {
            this.form = res
        })
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

                updateApiResource(this.form).then(res => {
                    this.$notify({
                        title: '成功',
                        message: '操作成功',
                        type: 'success',
                        duration: 2000
                    })
                    this.returnList()
                })
            });
        },
        handleManageScope(){
            this.$refs.updateApiResourceScopeDialog.showDialog(this.form.id)
        },
        handleCancel(){
            this.returnList()
        },
        returnList(){
            this.$router.push({
                name: 'Id4-ApiResource',
            })
        }
    }
}
</script>

<style scoped>

.action-bar{
    display: flex;
    justify-content: center;
    /* margin-bottom: 10px; */
    margin-top: 30px;
}

</style>