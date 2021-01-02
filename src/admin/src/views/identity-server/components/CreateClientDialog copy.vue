<template>
  <el-dialog     
    title="创建"
    widht="720"
    :visible.sync="dialogVisible">
    <el-form ref="form" :model="form" label-width="100px" size="small">
        <el-tabs type="border-card">
            <el-tab-pane label="基本">
                <el-form-item label="客户端Id">
                    <el-input v-model="form.clientId"></el-input>
                </el-form-item>
                <el-form-item label="客户端名称">
                    <el-input v-model="form.clientName"></el-input>
                </el-form-item>
                <el-form-item label="描述">
                    <el-input type="textarea" v-model="form.description"></el-input>
                </el-form-item>
                <el-form-item label="客户 Uri">
                    <el-input v-model="form.clientUri"></el-input>
                </el-form-item>
                <el-form-item label="Logo Uri">
                    <el-input v-model="form.logoUri"></el-input>
                </el-form-item>
                <el-form-item label="">
                    <el-checkbox :label="true" v-model="form.requireConsent">需要许可</el-checkbox>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Uris">Uris</el-tab-pane>
            <el-tab-pane label="Secrets">Secrets</el-tab-pane>
            <el-tab-pane label="资源">资源</el-tab-pane>
        </el-tabs>
     </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false" size="small">取 消</el-button>
        <el-button type="primary" @click="dialogVisible = false" size="small">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
export default {
    data(){
        return {
            form: {
                clientId: '',
                clientName: '',
                description: '',
                clientUri: '',
                logoUri: '',
                requireConsent: false
            },
            dialogVisible: false
        }
    },
    methods: {
        showDialog(){
            this.dialogVisible = true
        }
    }
}
</script>

<style>

</style>