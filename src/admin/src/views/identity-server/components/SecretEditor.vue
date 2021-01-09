<template>
    <el-card shadow="never">
        <el-row align="middle" justify="center" type="flex">
            <el-col :span="3" class="fieldName">类型</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-select v-model="clientSecret.type" style="width:100%">
                    <el-option label="SharedSecret" value="SharedSecret"></el-option>
                </el-select>
            </el-col>
            <el-col :span="3" class="fieldName">值</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-input v-model="clientSecret.value"> 
                    <template slot="append">
                        <el-button type="primary" @click="handleGenerateUUID">
                            <svg-icon icon-class="random" />
                        </el-button>
                    </template>
                </el-input>
            </el-col>
        </el-row>
        <el-row align="middle" justify="center" type="flex">
            <el-col :span="3" class="fieldName">描述</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-input v-model="clientSecret.description"></el-input>
            </el-col>
            <el-col :span="3" class="fieldName">过期</el-col>
            <el-col :span="7" :offset="1" class="fieldValue">
                <el-date-picker style="width:100%"
                    v-model="clientSecret.expiration"
                    type="date"
                    placeholder="选择日期">
                </el-date-picker>
            </el-col>
        </el-row>
        <el-row justify="end" type="flex">
            <el-button type="primary" @click="handleAdd">添加</el-button>
            <el-col :span="1" ></el-col>
        </el-row>

         <el-table :data="clientSecrets" style="width: 100%;margin-top:20px" border>
            <el-table-column prop="type" label="类型"></el-table-column>
            <el-table-column prop="value" label="值 （保存后，sha256）"></el-table-column>
            <el-table-column prop="description" label="描述"></el-table-column>
            <el-table-column prop="expiration" label="过期">
                <template slot-scope="{ row }">
                    <span v-if="row.expiration">{{ row.expiration | moment('yyyy-MM-DD') }}</span>
                </template>
            </el-table-column>
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
import { guid } from '@/utils'

const defaultClientSecret = {
    type: 'SharedSecret',
    value: '',
    description: '',
    expiration: undefined,
    isNew: true
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
        value(val){
            this.clientSecrets = _.cloneDeep(val || [])
        }
    },
    data(){
        return {
            clientSecret: _.cloneDeep(defaultClientSecret),
            clientSecrets: this.value
        }
    },
    methods: {
        handleAdd(){
            this.clientSecrets.push(this.clientSecret)
            this.clientSecret = _.cloneDeep(defaultClientSecret)
            this.onModelChange()
        },
        handleDelete(row, index){
            this.clientSecrets.splice(index, 1)
            this.onModelChange()
        },
        handleGenerateUUID(){
            this.clientSecret.value = guid()
        },
        onModelChange(){
            this.$emit('input', _.cloneDeep(this.clientSecrets))
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