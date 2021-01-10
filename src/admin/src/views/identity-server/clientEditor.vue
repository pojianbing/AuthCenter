<template>
  <el-card shadow="never">
    <el-form ref="form" :model="form" label-width="190px" size="small">
      <el-tabs class="center">
        <el-tab-pane label="名称">
          <el-form-item>
            <span slot="label">
              客户端标识
              <el-tooltip content="客户端的唯一标识" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.clientId" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              客户端名称
              <el-tooltip content="客户端显示名称（用于记录和同意屏幕）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.clientName" />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="基本">
          <el-form-item>
            <span slot="label">
              启用
              <el-tooltip content="指定是否启用客户端。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.enabled" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              客户端描述
              <el-tooltip content="客户端描述" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.description" type="textarea" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              协议类型
              <el-tooltip content="默认为 OpenID Connect 协议" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-select v-model="form.protocolType" style="width:100%">
              <el-option label="OpenID Connect" value="oidc" />
            </el-select>
          </el-form-item>
          <el-form-item>
            <span slot="label">
              需要客户端密钥
              <el-tooltip content="指定此客户端是否需要密钥才能从令牌端点请求令牌（默认为 true）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.requireClientSecret" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              需要 Pkce
              <el-tooltip content="指定使用基于授权代码的授权类型的客户端是否必须发送校验密钥" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.requirePkce" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许纯文本 Pkce
              <el-tooltip content="指定使用 PKCE 的客户端是否可以使用纯文本代码质询（不推荐 - 默认为 false）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.allowPlainTextPkce" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许离线访问
              <el-tooltip content="指定此客户端是否可以请求刷新令牌（请求 offline_access 作用域）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.allowOfflineAccess" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许通过浏览器访问令牌
              <el-tooltip content="指定是否允许此客户端通过浏览器接收访问令牌 access_token。 这对于强化允许多种响应类型的流是有用的（例如，通过禁止应该使用代码身份令牌 id_token 的混合流客户端来添加令牌响应类型并因此将令牌泄漏到浏览器。）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.allowAccessTokensViaBrowser" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许的作用域
              <el-tooltip content="默认情况下，客户端无权访问任何资源 - 通过添加相应的作用域名称来指定允许的资源" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.allowedScopes" type="claim" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              重定向 Uri
              <el-tooltip content="指定允许的 URI 以返回令牌或授权码" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.redirectUris" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许的授权类型
              <el-tooltip content="指定允许客户端使用的授权类型。 使用 GrantTypes 类进行常见组合。 默认授权列表：隐式授权 - (implicit)，客户端凭证授权 - (client_credentials)，授权码授权 - (authorization_code)，混合授权 - (hybrid)，资源所有者密码凭证授权 - (password)" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.allowedGrantTypes" type="grantType" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              客户端密钥
              <el-tooltip content="客户端密钥列表 - 访问令牌 access_token 端点的凭据。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SecretEditor v-model="form.clientSecrets" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              属性
              <el-tooltip content="字典可根据需要保存任何自定义客户端特定值。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <PropertyEditor v-model="form.properties" />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="认证/注销">
          <el-form-item>
            <span slot="label">
              前端通道注销 Uri
              <el-tooltip content="指定客户端的注销 URI，以用于基于 HTTP 的前端通道注销。 有关详细信息，请参阅 OIDC Front-Channel 规范。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.frontChannelLogoutUri" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              需要前端通道注销会话
              <el-tooltip content="指定是否应将用户的会话标识发送到 FrontChannelLogoutUri。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.frontChannelLogoutSessionRequired" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              后端通道退出 Uri
              <el-tooltip content="指定客户端的注销 URI，以用于基于 HTTP 的后端通道注销。 有关详细信息，请参阅 OIDC Back-Channel 规范。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.backChannelLogoutUri" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              需要后端通道注销会话
              <el-tooltip content="指定是否应在请求中将用户的会话 ID 发送到 BackChannelLogoutUri。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.backChannelLogoutSessionRequired" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              启用本地登录
              <el-tooltip content="指定此客户端是否可以使用本地帐户或仅外部 IdPs。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.enableLocalLogin" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              注销重定向 Uri
              <el-tooltip content="指定在注销后重定向到允许的 URI" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.postLogoutRedirectUris" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              身份提供程序限制
              <el-tooltip content="指定可以与此客户端一起使用的外部 IdPs（如果列表为空，则允许所有 IdPs）。 默认为空。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.identityProviderRestrictions" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              用户 SSO 生命周期
              <el-tooltip content="自上次用户进行身份验证以来的最长持续时间（以秒为单位）。 默认为 null" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.userSsoLifetime" type="number" />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="令牌">
          <el-form-item>
            <span slot="label">
              身份令牌生命周期
              <el-tooltip content="身份令牌 id_token 的生命周期（以秒为单位）（默认为 300 秒/5 分钟）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.identityTokenLifetime" type="number" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              访问令牌生命周期
              <el-tooltip content="访问令牌 access_token 的生命周期（以秒为单位）（默认为 3600 秒/1 小时）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.accessTokenLifetime" type="number" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              访问令牌类型
              <el-tooltip content="指定访问令牌 access_token 是引用令牌还是自包含 JWT 令牌（默认为 Jwt）。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-select v-model="form.accessTokenType" style="width:100%">
              <el-option label="Jwt" :value="0">Jwt</el-option>
              <el-option label="Reference" :value="1">Reference</el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <span slot="label">
              授权码生命周期
              <el-tooltip content="授权码的生命周期（以秒为单位）（默认为 300 秒/5 分钟）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.authorizationCodeLifetime" type="number" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              绝对刷新令牌生命周期
              <el-tooltip content="刷新令牌的最长生命周期，以秒为单位。 默认为 2592000 秒/30 天" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.absoluteRefreshTokenLifetime" type="number" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              滚动刷新令牌生命周期
              <el-tooltip content="滚动刷新生命周期是多少秒钟刷新令牌。 默认为 1296000 秒/15 天" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.slidingRefreshTokenLifetime" type="number" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              刷新令牌使用情况
              <el-tooltip content="ReUse 刷新令牌时，令牌句柄将保持不变 OneTime 刷新令牌时将更新令牌句柄" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-select v-model="form.refreshTokenUsage" style="width:100%">
              <el-option label="One Time Only" :value="0" />
              <el-option label="Reuse" :value="1" />
            </el-select>
          </el-form-item>
          <el-form-item>
            <span slot="label">
              刷新令牌过期
              <el-tooltip content="Absolute 绝对刷新令牌将在固定时间点到期（由“绝对刷新令牌生命周期”指定）Sliding 滚动刷新令牌，刷新令牌时生命周期将被更新（按“滚动刷新令牌生命周期”中指定的量）。生命周期不会超过“绝对刷新令牌生命周期”。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-select v-model="form.refreshTokenExpiration" style="width:100%">
              <el-option label="Absolute" :value="0" />
              <el-option label="Sliding" :value="1" />
            </el-select>
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许跨域来源
              <el-tooltip content="如果指定，将由默认 CORS 策略服务实现（In-Memory 和 EF）用于为 JavaScript 客户端构建 CORS 策略。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <SmartPicker v-model="form.allowedCorsOrigins" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              刷新时更新访问令牌声明
              <el-tooltip content="获取或设置一个值，该值指示是否应在刷新令牌请求上更新访问令牌 access_token（及其声明）。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.updateAccessTokenClaimsOnRefresh" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              包括 Jwt 标识
              <el-tooltip content="指定 JWT 访问令牌 access_token 是否应具有嵌入的唯一标识（通过 jti 声明）。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.includeJwtId" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              始终发送客户端声明
              <el-tooltip content="如果设置，将为每个流发送客户端声明。 如果不是，仅用于客户端凭证流（默认为 false）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.alwaysSendClientClaims" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              始终在身份令牌中包含用户声明
              <el-tooltip content="指定是否在身份令牌 id_token 中包含用户声明" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.alwaysIncludeUserClaimsInIdToken" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              客户端声明前缀
              <el-tooltip content="如果设置，客户端声明类型将以此为前缀。 默认为 client_。 目的是确保它们不会意外地与用户声明冲突。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.clientClaimsPrefix" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              配对主体盐
              <el-tooltip content="对于此客户端的用户，在配对主体标识生成中使用的盐值。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.pairWiseSubjectSalt" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              声明
              <el-tooltip content="允许客户端的设置声明（将包含在访问令牌 access_token 中）。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <ClaimsEditor v-model="form.claims" />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="同意屏幕">
          <el-form-item>
            <span slot="label">
              需要同意
              <el-tooltip content="指定是否需要同意屏幕。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.requireConsent" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              允许记住同意
              <el-tooltip content="指定用户是否可以选择存储同意决策。 默认为 true。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-switch v-model="form.allowRememberConsent" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              客户端 Uri
              <el-tooltip content="有关客户端的更多信息的URI（在同意屏幕上使用）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.clientUri" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              徽标 Uri
              <el-tooltip content="客户端徽标 URI（在同意屏幕上使用）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.logoUri" />
          </el-form-item>
        </el-tab-pane>

        <el-tab-pane label="设备流程">
          <el-form-item>
            <span slot="label">
              用户代码类型
              <el-tooltip content="指定用于客户端的用户代码的类型。 否则回落到默认值。" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.userCodeType" />
          </el-form-item>
          <el-form-item>
            <span slot="label">
              设备代码生命周期
              <el-tooltip content="设备代码的生命周期（以秒为单位）（默认为 300 秒/5 分钟）" placement="top">
                <i class="el-alert__icon el-icon-info" />
              </el-tooltip>
            </span>
            <el-input v-model="form.deviceCodeLifetime" type="number" />
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
import ClaimsEditor from './components/ClaimsEditor'
import PropertyEditor from './components/PropertyEditor'
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
  components: { SecretEditor, ClaimsEditor, PropertyEditor, SmartPicker },
  data() {
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
    handleSave() {
      updateClient(this.form).then(res => {
        this.$notify({
          title: '成功',
          message: '操作成功',
          type: 'success',
          duration: 2000
        })
      })
    },
    handleCancel() {
      this.returnList()
    },
    returnList() {
      this.$router.push({
        name: 'Id4-Clients'
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
