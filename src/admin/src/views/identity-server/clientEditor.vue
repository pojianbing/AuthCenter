<template>
    <el-card shadow="never">
        <el-form ref="form" :model="form" label-width="190px" size="small">
            <el-tabs class="center">
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
                    <el-form-item>
                        <span slot="label">
                            允许的作用域 
                            <el-tooltip content="默认情况下，客户端无权访问任何资源 - 通过添加相应的作用域名称来指定允许的资源" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SmartPicker type="claim" v-model="form.allowedScopes"></SmartPicker>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            重定向 Uri 
                            <el-tooltip content="指定允许的 URI 以返回令牌或授权码" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SmartPicker v-model="form.redirectUris"></SmartPicker>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            允许的授权类型
                            <el-tooltip content="指定允许客户端使用的授权类型。 使用 GrantTypes 类进行常见组合。 默认授权列表：隐式授权 - (implicit)，客户端凭证授权 - (client_credentials)，授权码授权 - (authorization_code)，混合授权 - (hybrid)，资源所有者密码凭证授权 - (password)" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SmartPicker type="grantType"  v-model="form.allowedGrantTypes"></SmartPicker>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            客户端密钥
                            <el-tooltip content="客户端密钥列表 - 访问令牌 access_token 端点的凭据。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SecretEditor v-model="form.clientSecrets"></SecretEditor>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            属性
                            <el-tooltip content="字典可根据需要保存任何自定义客户端特定值。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <ClientPropertyEditor v-model="form.properties"></ClientPropertyEditor>
                    </el-form-item>
                </el-tab-pane>
                <el-tab-pane label="认证/注销">
                    <el-form-item>
                        <span slot="label">
                            前端通道注销 Uri 
                            <el-tooltip content="指定客户端的注销 URI，以用于基于 HTTP 的前端通道注销。 有关详细信息，请参阅 OIDC Front-Channel 规范。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-input v-model="form.frontChannelLogoutUri"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            需要前端通道注销会话 
                            <el-tooltip content="指定是否应将用户的会话标识发送到 FrontChannelLogoutUri。 默认为 true。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-switch v-model="form.frontChannelLogoutSessionRequired"></el-switch>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            后端通道退出 Uri
                            <el-tooltip content="指定客户端的注销 URI，以用于基于 HTTP 的后端通道注销。 有关详细信息，请参阅 OIDC Back-Channel 规范。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-input v-model="form.backChannelLogoutUri"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            需要后端通道注销会话 
                            <el-tooltip content="指定是否应在请求中将用户的会话 ID 发送到 BackChannelLogoutUri。 默认为 true。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-switch v-model="form.backChannelLogoutSessionRequired"></el-switch>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            启用本地登录 
                            <el-tooltip content="指定此客户端是否可以使用本地帐户或仅外部 IdPs。 默认为 true。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-switch v-model="form.enableLocalLogin"></el-switch>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            注销重定向 Uri 
                            <el-tooltip content="指定在注销后重定向到允许的 URI" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SmartPicker v-model="form.postLogoutRedirectUris"></SmartPicker>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            身份提供程序限制
                            <el-tooltip content="指定可以与此客户端一起使用的外部 IdPs（如果列表为空，则允许所有 IdPs）。 默认为空。" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <SmartPicker v-model="form.identityProviderRestrictions"></SmartPicker>
                    </el-form-item>
                    <el-form-item>
                        <span slot="label">
                            用户 SSO 生命周期
                            <el-tooltip content="自上次用户进行身份验证以来的最长持续时间（以秒为单位）。 默认为 null" placement="top">
                                <i class="el-alert__icon el-icon-info"></i>
                            </el-tooltip>
                        </span>
                        <el-input v-model="form.backChannelLogoutUri" type="number"></el-input>
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
                    <el-form-item label="Require Consent Screen">
                        <el-checkbox v-model="form.requireConsent" label="是"></el-checkbox>
                    </el-form-item>
                    <el-form-item label="Remember Consent">
                        <el-checkbox v-model="form.allowRememberConsent" label="是"></el-checkbox>
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
            </el-tabs>

            <div class="action-bar">
                <el-button size="small" @click="handleCancel">返 回</el-button>
                <el-button type="primary" size="small" @click="handleSave">保 存</el-button>
            </div>
        </el-form>
    </el-card>
</template>

<script>
import _ from 'lodash'
import {
  updateClient, getClient
} from '@/api/identity-server/client'
import SecretEditor from './components/SecretEditor'
import ClientClaimsEditor from './components/ClientClaimsEditor'
import ClientPropertyEditor from './components/ClientPropertyEditor'
import SmartPicker from './components/SmartPicker'

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
            defaultGrantTypes,
            clientSecret: _.cloneDeep(defaultClientSecret)
        }
    },
    mounted() {
        getClient(this.$route.query.id).then(res => {
            this.form = res
        })
    },
    methods: {
        handleSave(){
            updateClient(this.form).then(res => {
                this.$notify({
                    title: '成功',
                    message: '操作成功',
                    type: 'success',
                    duration: 2000
                })
            })
        },
        handleCancel(){
            this.returnList()
        },
        returnList(){
            this.$router.push({
                name: 'Id4-Clients',
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