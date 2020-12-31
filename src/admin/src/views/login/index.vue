<template>
  <div class="login-container">
    <vue-particles
      color="#fff"
      :particle-opacity="0.9"
      :particles-number="50"
      shape-type="star"
      :particle-size="4"
      lines-color="#fff"
      :lines-width="1"
      :line-linked="true"
      :line-opacity="0.4"
      :lines-distance="150"
      :move-speed="2"
      :hover-effect="true"
      hover-mode="grab"
      :click-effect="true"
      click-mode="push"
      class="background"
    />
    <div class="loginPanel">
      <el-image
        :src="logoPanelBg"
        fit="fill"
        class="beauty"
      />
      <div class="flex flex-col">
        <el-form
          v-if="isLogin"
          ref="loginForm"
          :model="loginForm"
          :rules="loginRules"
          class="login-form"
          autocomplete="on"
          label-position="left"
        >
          <div class="title-container">
            <h3 class="title">欢迎登录</h3>
          </div>
          <el-form-item prop="username">
            <el-input
              ref="username1"
              key="loginForm.username"
              v-model="loginForm.username"
              placeholder="手机号"
              name="username"
              type="text"
              class="simple"
              tabindex="1"
              autocomplete="on"
            />
          </el-form-item>

          <el-tooltip v-model="capsTooltip" content="Caps lock is On" placement="right" manual>
            <el-form-item prop="password">
              <el-input
                ref="password1"
                key="password1"
                v-model="loginForm.password"
                :type="passwordType1"
                placeholder="密码"
                name="password"
                class="simple"
                tabindex="2"
                autocomplete="on"
                @keyup.native="checkCapslock"
                @blur="capsTooltip = false"
                @keyup.enter.native="handleLogin"
              />
              <span class="show-pwd" @click="showPwd1">
                <svg-icon :icon-class="passwordType1 === 'password' ? 'eye' : 'eye-open'" />
              </span>
            </el-form-item>
          </el-tooltip>

          <el-button
            :loading="loading"
            round
            type="primary"
            style="width:100%;margin-bottom:30px;"
            @click.native.prevent="handleLogin"
          >确定</el-button>
        </el-form>
        <el-form
          v-else
          ref="registerForm"
          :model="registerForm"
          :rules="registerRules"
          class="login-form"
          autocomplete="on"
          label-position="left"
        >
          <div class="title-container">
            <h3 class="title">注册</h3>
          </div>
          <el-form-item prop="username">
            <el-input
              ref="username2"
              v-model="registerForm.username"
              placeholder="手机号"
              name="username"
              class="simple"
              type="text"
              tabindex="1"
              autocomplete="on"
            />
          </el-form-item>
          <el-form-item prop="verifyCode" style="position:relative">
            <el-input
              v-model="registerForm.verifyCode"
              placeholder="验证码"
              name="verifyCode"
              class="simple"
              type="text"
              tabindex="1"
              autocomplete="on"
              maxlength="11"
            />
            <el-button type="primary" plain size="mini" class="send-code-button"> 发送验证码</el-button>
          </el-form-item>
          <el-tooltip v-model="capsTooltip" content="Caps lock is On" placement="right" manual>
            <el-form-item prop="password">
              <el-input
                ref="password2"
                key="password2"
                v-model="registerForm.password"
                :type="passwordType2"
                placeholder="设置密码：6-16位，必须包含字母和数字"
                name="password"
                class="simple"
                tabindex="2"
                autocomplete="on"
                @keyup.native="checkCapslock"
                @blur="capsTooltip = false"
                @keyup.enter.native="handleLogin"
              />
              <span class="show-pwd" @click="showPwd2">
                <svg-icon :icon-class="passwordType2 === 'password' ? 'eye' : 'eye-open'" />
              </span>
            </el-form-item>
          </el-tooltip>

          <el-button
            :loading="loading"
            round
            type="primary"
            style="width:100%;margin-bottom:30px;"
            @click.native.prevent="handleRegister"
          >确定</el-button>
        </el-form>
        <div v-if="isLogin" class="flex items-center justify-center">
          <span class="text-center text-sm">还没账号？</span>
          <el-link class="text-sm" :underline="false" type="primary" @click="handleChangeViewState(1)">注册</el-link>
        </div>
        <div v-else class="flex items-center justify-center">
          <span class="text-center text-sm">已有账号？</span>
          <el-link class="text-sm" :underline="false" type="primary" @click="handleChangeViewState(2)">登录</el-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { validUsername } from '@/utils/validate'
import logoPanelBg from '@/assets/login-panel-bg.png'

export default {
  name: 'Login',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (!validUsername(value)) {
        callback(new Error('Please enter the correct user name'))
      } else {
        callback()
      }
    }
    const validatePassword = (rule, value, callback) => {
      if (value.length < 6) {
        callback(new Error('密码不少于6个字符'))
      } else {
        callback()
      }
    }
    return {
      loginForm: {
        tenant: '',
        username: 'admin',
        password: '1q2w3E*'
      },
      loginRules: {
        username: [{ trigger: 'blur', validator: validateUsername }],
        password: [{ trigger: 'blur', validator: validatePassword }]
      },
      registerForm: {
        username: '',
        password: ''
      },
      registerRules: {
        username: [{ trigger: 'blur', validator: validateUsername }],
        password: [{ trigger: 'blur', validator: validatePassword }]
      },
      passwordType1: 'password',
      passwordType2: 'password',
      capsTooltip: false,
      loading: false,
      showDialog: false,
      redirect: undefined,
      otherQuery: {},
      logoPanelBg,
      isLogin: true
    }
  },
  watch: {
    $route: {
      handler: function(route) {
        const query = route.query
        if (query) {
          this.redirect = query.redirect
          this.otherQuery = this.getOtherQuery(query)
        }
      },
      immediate: true
    }
  },
  created() {
    // window.addEventListener('storage', this.afterQRScan)
  },
  mounted() {
    if (this.loginForm.username === '') {
      this.$refs.username1.focus()
    } else if (this.loginForm.password === '') {
      this.$refs.password1.focus()
    }
  },
  destroyed() {
    // window.removeEventListener('storage', this.afterQRScan)
  },
  methods: {
    checkCapslock(e) {
      const { key } = e
      this.capsTooltip = key && key.length === 1 && (key >= 'A' && key <= 'Z')
    },
    showPwd1() {
      if (this.passwordType1 === 'password') {
        this.passwordType1 = ''
      } else {
        this.passwordType1 = 'password'
      }
      this.$nextTick(() => {
        this.$refs.password1.focus()
      })
    },
    showPwd2() {
      if (this.passwordType2 === 'password') {
        this.passwordType2 = ''
      } else {
        this.passwordType2 = 'password'
      }
      this.$nextTick(() => {
        this.$refs.password2.focus()
      })
    },
    handleChangeViewState(state) {
      this.isLogin = state === 2

      this.$nextTick(() => {
        this.$refs.loginForm && this.$refs.loginForm.clearValidate()
        this.$refs.registerForm && this.$refs.registerForm.clearValidate()
      })
    },
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store
            .dispatch('user/login', this.loginForm)
            .then(() => {
              this.$router.push({
                path: this.redirect || '/',
                query: this.otherQuery
              })
              this.loading = false
            })
            .catch((error) => {
              console.error(error)
              this.loading = false
            })
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    handleRegister() {
      // this.$refs.registerForm.validate(valid => {
      //   if (valid) {
      //     register(this.registerForm).then(res => {
      //       console.log('注册')
      //     })
      //   } else {
      //     return false
      //   }
      // })
    },
    getOtherQuery(query) {
      return Object.keys(query).reduce((acc, cur) => {
        if (cur !== 'redirect') {
          acc[cur] = query[cur]
        }
        return acc
      }, {})
    }
  }
}
</script>

<style lang="scss">
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg: #283443;
$light_gray: #000;
$cursor: #000;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .login-container .el-input input {
    color: $cursor;
  }
}

/* reset element-ui css */
.login-container {
  background: #f0f2f5 url('../../assets/background1.jpg') no-repeat 50%;
  .el-input {
    display: inline-block;
    height: 47px;
    width: 100%;

    input {
      border-radius: 3px;
      padding: 12px 5px 12px 15px;
      caret-color: $cursor;
    }
  }

  .el-form-item {
    border: 1px solid rgba(255, 255, 255, 0.1);
    background: rgba(255, 255, 255, 0.1);
    border-radius: 5px;
    color: white;
  }
}
</style>

<style lang="scss" scoped>
$bg: #2d3a4b;
$dark_gray: #889aa4;
$light_gray: #1890ff;

.login-container {
  min-height: 100%;
  width: 100%;
  height: 100%;
  background-color: $bg;
  overflow: hidden;
  display: flex;
  flex-direction: column;

  .loginPanel {
    margin: 0 auto;
    margin-top: 160px;
    padding: 20px;
    background-color: #ffffff;
    box-shadow: 4px 4px 8px 0px #cacaca82;
    z-index: 1;
    border-radius: 8px;
    display: flex;
    height: 480px;

    .beauty{
      width: 400px;
      border-radius: 5px;
    }
  }

  .login-form {
    position: relative;
    width: 440px;
    max-width: 100%;
    padding: 0px 35px 0;
    margin: 0 auto;
    overflow: hidden;
  }

  .tips {
    font-size: 14px;
    color: #fff;
    margin-bottom: 10px;

    span {
      &:first-of-type {
        margin-right: 16px;
      }
    }
  }

  .icon-container {
    padding: 6px 5px 6px 15px;
    color: $light_gray;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
  }

  .title-container {
    position: relative;
    margin-bottom: 80px;

    .title {
      font-size: 26px;
      color: black;
      margin: 0px auto 40px auto;
      text-align: center;
      font-weight: bold;
    }
  }

  .show-pwd {
    position: absolute;
    right: 0px;
    top: 7px;
    font-size: 16px;
    color: $light_gray;
    cursor: pointer;
    user-select: none;
  }

  .send-code-button{
    position: absolute;
    right: 0px;
  }

  .thirdparty-button {
    position: absolute;
    right: 0;
    bottom: 6px;
  }

  @media only screen and (max-width: 470px) {
    .thirdparty-button {
      display: none;
    }
  }

  .background{
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
  }

  .text-center {
    text-align: center;
  }

  .text-sm {
    font-size: 0.875rem;
  }

  .justify-center {
    -webkit-box-pack: center;
    -ms-flex-pack: center;
    justify-content: center;
  }

  .items-center {
      -webkit-box-align: center;
      -ms-flex-align: center;
      align-items: center;
  }

  .flex {
      display: -webkit-box;
      display: -ms-flexbox;
      display: flex;
  }

  .flex-col {
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    -ms-flex-direction: column;
    flex-direction: column;
  }
}
</style>
