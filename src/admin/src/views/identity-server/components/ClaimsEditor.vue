<template>
    <el-card shadow="never">
        <el-row align="middle" justify="center" type="flex">
            <el-col :span="3" class="fieldName">类型</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-input v-model="clientClaim.type"></el-input>
            </el-col>
            <el-col :span="3" class="fieldName">值</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-input v-model="clientClaim.value"></el-input>
            </el-col>
        </el-row>
        <el-row justify="end" type="flex">
            <el-button type="primary" @click="handleAdd">添加</el-button>
            <el-col :span="1" ></el-col>
        </el-row>

         <el-table :data="clientClaims" style="width: 100%;margin-top:20px" border>
            <el-table-column prop="type" label="类型"></el-table-column>
            <el-table-column prop="value" label="值"></el-table-column>
            <el-table-column
                fixed="right"
                label="操作"
                width="100">
                <template slot-scope="{ row, $index }">
                    <el-button @click="handleDelete(row, $index)" type="text" size="mini">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-card>
</template>

<script>
import _ from 'lodash'

const defaultClientClaim = {
    type: '',
    value: ''
}

export default {
    props: {
        value:{
            type: Array,
            isRequired: true, 
            default(){
                return []
            }
        }
    },
    watch: {
        value: {
            handler(val){
                this.clientClaims = _.cloneDeep(val || [])
            },
            immediate: true
        }
    },
    data(){
        return {
            clientClaim: _.cloneDeep(defaultClientClaim),
            clientClaims: []
        }
    },
    methods: {
        handleAdd(){
            this.clientClaims.push(this.clientClaim)
            this.clientClaim = _.cloneDeep(defaultClientClaim)
            this.onModelChange()
        },
        handleDelete(row, index){
            this.clientClaims.splice(index, 1)
            this.onModelChange()
        },
        onModelChange(){
            this.$emit('input', _.cloneDeep(this.clientClaims))
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