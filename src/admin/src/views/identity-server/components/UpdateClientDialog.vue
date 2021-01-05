<template>
  <el-dialog 
    custom-class="custom"
    title="更新"
    width="880px"
    :close-on-click-modal="false"
    :visible.sync="dialogVisible">
    <el-form ref="form" :model="form" label-width="190px" size="small">
        <el-tabs type="border-card">
            <el-tab-pane label="名称">
                <el-form-item>
                    <span slot="label">
                        客户端标识 
                        <el-tooltip content="客户端的唯一标识" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-input v-model="form.clientId"></el-input>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        客户端名称 
                        <el-tooltip content="客户端显示名称（用于记录和同意屏幕）" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-input v-model="form.clientName"></el-input>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        测试 
                        <el-tooltip content="默认情况下，客户端无权访问任何资源 - 通过添加相应的作用域名称来指定允许的资源" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <SmartPicker type="claim"></SmartPicker>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="基本">
                <el-form-item>
                    <span slot="label">
                        启用 
                        <el-tooltip content="指定是否启用客户端。 默认为 true。" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.enabled"></el-switch>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        客户端描述 
                        <el-tooltip content="客户端描述" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-input type="textarea" v-model="form.description"></el-input>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        协议类型 
                        <el-tooltip content="默认为 OpenID Connect 协议" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-select v-model="form.protocolType" style="width:100%">
                        <el-option label="OpenID Connect" value="oidc"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        需要客户端密钥 
                        <el-tooltip content="指定此客户端是否需要密钥才能从令牌端点请求令牌（默认为 true）" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.requireClientSecret"></el-switch>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        需要 Pkce 
                        <el-tooltip content="指定使用基于授权代码的授权类型的客户端是否必须发送校验密钥" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.requirePkce"></el-switch>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        允许纯文本 Pkce 
                        <el-tooltip content="指定使用 PKCE 的客户端是否可以使用纯文本代码质询（不推荐 - 默认为 false）" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.allowPlainTextPkce"></el-switch>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        允许离线访问
                        <el-tooltip content="指定此客户端是否可以请求刷新令牌（请求 offline_access 作用域）" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.allowOfflineAccess"></el-switch>
                </el-form-item>
                <el-form-item>
                    <span slot="label">
                        允许通过浏览器访问令牌 
                        <el-tooltip content="指定是否允许此客户端通过浏览器接收访问令牌 access_token。 这对于强化允许多种响应类型的流是有用的（例如，通过禁止应该使用代码身份令牌 id_token 的混合流客户端来添加令牌响应类型并因此将令牌泄漏到浏览器。）" placement="top">
                            <i class="el-alert__icon el-icon-info"></i>
                        </el-tooltip>
                    </span>
                    <el-switch v-model="form.allowAccessTokensViaBrowser"></el-switch>
                </el-form-item>
                <el-form-item label="Allowed Scopes">
                    <el-select style="width: 100%"
                        v-model="form.allowedScopes"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add Scope">
                        <el-option
                            v-for="item in form.allowedScopes"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Allowed Grant Types">
                    <el-select style="width: 100%"
                        v-model="form.allowedGrantTypes"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add Grant">
                        <el-option
                            v-for="item in form.allowedGrantTypes"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                    <el-tag 
                        type="info" 
                        style="margin-right:5px;cursor: pointer;" 
                        v-for="(item, idx) in defaultGrantTypes" 
                        :key="idx" 
                        @click="handleDefaultGrandAdd(item)">
                        {{item}} <i class="el-icon-circle-plus-outline"/>
                   </el-tag>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Basic">
                <el-form-item label="客户 Uri">
                    <el-input v-model="form.clientUri"></el-input>
                </el-form-item>
                <el-form-item label="Logo Uri">
                    <el-input v-model="form.logoUri"></el-input>
                </el-form-item>
                <el-form-item label="Allowed Cors Origins">
                    <el-select style="width: 100%"
                        v-model="form.allowedCorsOrigins"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add Allowed Cors Origins">
                        <el-option
                            v-for="item in form.allowedCorsOrigins"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Settings">

                <el-form-item label="Redirect Uri's">
                    <el-select style="width: 100%"
                        v-model="form.redirectUris"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add Url">
                        <el-option
                            v-for="item in form.redirectUris"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Require Consent Screen">
                    <el-checkbox v-model="form.requireConsent" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Remember Consent">
                    <el-checkbox v-model="form.allowRememberConsent" label="是"></el-checkbox>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Secrets">
                <SecretEditor v-model="form.clientSecrets"></SecretEditor>
            </el-tab-pane>
            <el-tab-pane label="Authentication">
                <el-form-item label="Enable Local Login">
                    <el-checkbox v-model="form.enableLocalLogin" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Identity Provider Restrictions">
                    <el-select style="width: 100%"
                        v-model="form.identityProviderRestrictions"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add New Tag">
                        <el-option
                            v-for="item in form.identityProviderRestrictions"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Logout Url">
                    <el-select style="width: 100%"
                        v-model="form.postLogoutRedirectUris"
                        multiple
                        filterable
                        allow-create
                        default-first-option
                        placeholder="Add New Tag">
                        <el-option
                            v-for="item in form.postLogoutRedirectUris"
                            :key="item"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Front Channel Logout Uri">
                    <el-input v-model="form.frontChannelLogoutUri" placeholder="Specifies logout URI at client for HTTP based front-channel logout. See the OIDC Front-Channel spec for more details"></el-input>
                </el-form-item>
                <el-form-item label="Front Channel Logout Session">
                    <el-checkbox v-model="form.frontChannelLogoutSessionRequired" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Back Channel Logout Uri">
                    <el-input v-model="form.backChannelLogoutUri" placeholder="Specifies logout URI at client for HTTP based back-channel logout. See the OIDC Back-Channel spec for more details"></el-input>
                </el-form-item>
                <el-form-item label="Back Channel Logout Session">
                    <el-checkbox v-model="form.backChannelLogoutSessionRequired" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="User Sso Lifetime(in seconds)">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.userSsoLifetime" placeholder="The maximum duration (in seconds) since the last time the user authenticated. Defaults to null. You can adjust the lifetime of a session token to control when and how often a user is required to reenter credentials instead of being silently authenticated, when using a web application"></el-input-number>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Token">
                <el-form-item label="Identity Token Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.identityTokenLifetime" ></el-input-number>
                </el-form-item>
                <el-form-item label="Access Token Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.accessTokenLifetime" ></el-input-number>
                </el-form-item>
                <el-form-item label="Access Token Type">
                    <el-select v-model="form.accessTokenType" style="width:100%">
                        <el-option label="Jwt" :value="0">Jwt</el-option>
                        <el-option label="Reference" :value="1">Reference</el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Authorization Code Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.authorizationCodeLifetime" ></el-input-number>
                </el-form-item>
                <el-form-item label="Absolute Refresh Token Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.absoluteRefreshTokenLifetime" ></el-input-number>
                </el-form-item>
                <el-form-item label="Sliding Refresh Token Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.slidingRefreshTokenLifetime" ></el-input-number>
                </el-form-item>
                <el-form-item label="Refresh Token Usage">
                    <el-select v-model="form.refreshTokenUsage" style="width:100%">
                        <el-option label="One Time Only" :value="0"></el-option>
                        <el-option label="Reuse" :value="1"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Refresh Token Expiration">
                    <el-select v-model="form.refreshTokenExpiration" style="width:100%">
                        <el-option label="Absolute" :value="0"></el-option>
                        <el-option label="Sliding" :value="1"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Update Access Token Claims On Refresh">
                    <el-checkbox v-model="form.updateAccessTokenClaimsOnRefresh" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Include Jwt Id">
                    <el-checkbox v-model="form.includeJwtId" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Always Send Client Claims">
                    <el-checkbox v-model="form.alwaysSendClientClaims" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Always Include User Claims In IdToken">
                    <el-checkbox v-model="form.alwaysIncludeUserClaimsInIdToken" label="是"></el-checkbox>
                </el-form-item>
                <el-form-item label="Pair Wise Subject Salt">
                    <el-input v-model="form.pairWiseSubjectSalt"></el-input>
                </el-form-item>
                <el-form-item label="Client Claims Prefix">
                    <el-input v-model="form.clientClaimsPrefix"></el-input>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Claims">
                <ClientClaimsEditor v-model="form.claims"></ClientClaimsEditor>
            </el-tab-pane>
            <el-tab-pane label="DeviceFlow">
                <el-form-item label="User Code Type">
                    <el-input v-model="form.userCodeType"></el-input>
                </el-form-item>
                <el-form-item label="Device Code Lifetime">
                    <el-input-number class="custom" style="width: 100%;" :controls="false" v-model="form.deviceCodeLifetime" ></el-input-number>
                </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="Properties">
                <ClientPropertyEditor v-model="form.properties"></ClientPropertyEditor>
            </el-tab-pane>
        </el-tabs>
     </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false" size="small">取 消</el-button>
        <el-button type="primary" @click="handleSave" size="small">保 存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import _ from 'lodash'
import {
  updateClient, getClient
} from '@/api/identity-server/client'
import SecretEditor from './SecretEditor'
import ClientClaimsEditor from './ClientClaimsEditor'
import ClientPropertyEditor from './ClientPropertyEditor'
import SmartPicker from './SmartPicker'

const defaultGrantTypes = [
    'authorization_code', 
    'client_credentials', 
    'urn:ietf:params:oauth:grant-type:device_code', 
    'hybrid', 
    'implicit', 
    'password' 
]

const defaultClientSecret = {
    type: 'SharedSecret',
    value: '',
    description: '',
    expiration: undefined
}

export default {
    components: { SecretEditor, ClientClaimsEditor, ClientPropertyEditor, SmartPicker },
    data(){
        return {
            form: {
                allowedCorsOrigins: [],
                allowedScopes: [],
                redirectUris: [],
                allowedGrantTypes: [],
                clientSecrets: [],
                identityProviderRestrictions: [],
                postLogoutRedirectUris: [],
                claims: [],
                properties: []
            },
            dialogVisible: false,
            defaultGrantTypes,
            clientSecret: _.cloneDeep(defaultClientSecret)
        }
    },
    methods: {
        handleDefaultGrandAdd(grant){
            const isExist = this.form.allowedGrantTypes.findIndex(e => e === grant) !== -1
            if(isExist) return 

            this.form.allowedGrantTypes.push(grant)
        },
        handleSave(){
            console.log(this.form)
            updateClient(this.form).then(res => {
                this.$notify({
                    title: '成功',
                    message: '操作成功',
                    type: 'success',
                    duration: 2000
                })
                this.dialogVisible = false
                this.$emit('success')
            })
        },
        showDialog(id){
            this.dialogVisible = true

            getClient(id).then(res => {
                this.form = res
            })
        }
    }
}
</script>

<style>

</style>