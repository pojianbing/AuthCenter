<template>
    <div class="container">
        <!-- <el-row :gutter="gutter">
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[0]" @click.native="handleSelect(0)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[1]" @click.native="handleSelect(1)"></ClientTypeSelectorCell>
            </el-col>
        </el-row>
        <el-row :gutter="gutter">
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[2]" @click.native="handleSelect(2)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[3]" @click.native="handleSelect(3)"></ClientTypeSelectorCell>
            </el-col>
        </el-row>
        <el-row :gutter="gutter">
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[4]" @click.native="handleSelect(4)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="12" >
                <ClientTypeSelectorCell :data="clientTypes[5]" @click.native="handleSelect(5)"></ClientTypeSelectorCell>
            </el-col>
        </el-row> -->
        <el-row :gutter="gutter">
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[0]" @click.native="handleSelect(0)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[1]" @click.native="handleSelect(1)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[2]" @click.native="handleSelect(2)"></ClientTypeSelectorCell>
            </el-col>
        </el-row>
        <el-row :gutter="gutter">
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[3]" @click.native="handleSelect(3)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[4]" @click.native="handleSelect(4)"></ClientTypeSelectorCell>
            </el-col>
            <el-col :span="8" >
                <ClientTypeSelectorCell :data="clientTypes[5]" @click.native="handleSelect(5)"></ClientTypeSelectorCell>
            </el-col>
        </el-row>
    </div>    
</template>

<script>

const clientTypes = [
    { name: '空 - 默认', desc: '',  icon: 'empty', value: 0, checked: true},
    { name: '单页应用程序 - Javascript', desc: 'Authorization Code Flow with PKCE',  icon: 'computer', value: 3, checked: false },
    { name: 'Web 应用程序 - 服务器端', desc: 'Authorization Code Flow with PKCE',  icon: 'webserver', value: 2, checked: false },
    { name: '电视和限制输入设备应用程序', desc: '设备流程',  icon: 'tv', value: 1, checked: false, checked: false },
    { name: '原生应用程序 - 移动/桌面', desc: 'Authorization Code Flow with PKCE',  icon: 'mobile', value: 5, checked: false },
    { name: '机械/机器人', desc: '资源所有者密码和客户端凭据流',  icon: 'robot', value: 6, checked: false }
] 

import ClientTypeSelectorCell from './ClientTypeSelectorCell'

export default {
    components: { ClientTypeSelectorCell },
    props: {
        value:{
            type: Number,
            isRequired: true,
            default(){
                return 0
            }
        }
    },
    data(){
        return {
            clientTypes,
            gutter: 20
        }
    },
    watch: {
        value: {
            handler(newval, old){
                this.clientTypes.forEach(clientType => {
                    clientType.checked = clientType.value === newval
                })
            },
            immediate: true
        }
    },
    methods: {
        handleSelect(index){
            this.deselectAll()
            this.clientTypes[index].checked = true
            this.$emit('input', this.clientTypes[index].value)
        },
        deselectAll(){
            this.clientTypes.forEach(clientType => {
                clientType.checked = false
            })
        }
    }
}
</script>

<style scoped>

    .container .el-row {
        margin-top:20px;
    }



</style>