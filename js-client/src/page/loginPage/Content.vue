<template>
  <div class="loginWrap">
    <h2 class="tit">华信期货知识库（Wiki）</h2>
    <div class="loginLogo1"><img src="../../assets/image/logo2.png" width="120"/></div>
    <div class="loginFrom">
      <input type="text" v-model="formCustom.username" class="userName" placeholder="用户名">
      <input type="password" v-model="formCustom.passwd" class="passWord" placeholder="用户密码">
      <Button type="primary" @click="login('formCustom')" class="loginBtn">登录</Button>
    </div>
  </div>
</template>
<script>
//  import { identitySiteRootSite } from '../../../static/js/config/site-config'
/* eslint-disable */
  export default {
    name: 'content',
    components: {},
    data () {
      const validatePass = (rule, value, callback) => {
        if (value.length < 6) {
          callback(new Error('密码不能少于6位'))
        }
        if (value === '') {
          callback(new Error('请输入密码'))
        } else {
          callback()
        }
      }
      return {
        ruleCustom: {
          passwd: [
            {validator: validatePass, trigger: 'blur'}
          ],
          username: [
//            {validator: , trigger: 'blur'}
          ]
        },
        formCustom: {
          passwd: '111111',
          username: 'admin'
        },
        url: localStorage.getItem('identitySiteRootSite'),
        client_id: 'Wpbs',
        client_secret: 'secret',
        access_token: '',
        refresh_token: '',
        show: true
      }
    },
    computed: {},
    mounted: function () {
      var logo = document.querySelector('.loginLogo1')
      logo.style.transform = 'scale(0)'
      logo.style.WebkitTransform = 'scale(0)'
      setTimeout(function () {
        logo.style.transform = 'scale(1)'
        logo.style.WebkitTransform = 'scale(1)'
        logo.style.transition = '2s'
        logo.style.WebkitTransition = '2s'
        setTimeout(function () {
          logo.style.animation = '2s rotate linear infinite'
          logo.style.WebkitAnimation = '2s rotate linear infinite'
        }, 2000)
      }, 1300)
      $('.ivu-modal-mask').css({
        'z-index': -1
      })
    },
    methods: {
      login () {
        var _self = this
        var auth = 'Basic ' + btoa(this.client_id + ':' + this.client_secret)
        var data = {
          username: this.formCustom.username,
          password: this.formCustom.passwd,
          grant_type: 'password',
          scope: 'openid profile roles rs_userInfo offline_access'
        }
        var body = ''
        for (var key in data) {
          if (body.length) {
            body += '&'
          }
          body += key + '='
          body += encodeURIComponent(data[key])
        }

        var xhr = new XMLHttpRequest()
        xhr.onload = function (e) {
          let responseData = JSON.parse(xhr.response)
          if (xhr.status === 200 && responseData.access_token) {
            let expiresIn = responseData.expires_in
            _self.access_token = responseData.access_token
            _self.refresh_token = responseData.refresh_token
            _self.getUser(_self.access_token, expiresIn, _self.refresh_token)
          } else {
            _self.$Modal.error({
              title: '错误信息',
              content: '登录名或密码错误，请重试'
            })
          }
        }
        xhr.open('POST', _self.url + '/identity/connect/token')
        xhr.setRequestHeader('Authorization', auth)
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
        xhr.send(body)
      },
      getUser (accessToken, expiresIn, refreshtoken) {
        let _this = this
        var url = _this.url + '/identity/connect/userinfo'
        var xhr = new XMLHttpRequest()
        xhr.onload = function (e) {
          var responseData = JSON.parse(xhr.response)
          let userProfile = {}
          userProfile.profile = responseData
          var timestamp = (new Date()).getTime() / 1000 + expiresIn
          let user = Object.assign({}, userProfile, {access_token: accessToken}, {expires_at: timestamp}, {refresh_token: refreshtoken})
          if (xhr.status === 200) {
            const STORAGE_IDENTITY_KEY = 'STORAGE_IDENTITY'
            localStorage.setItem(STORAGE_IDENTITY_KEY, JSON.stringify(user))
            _this.$store.dispatch('loginLater', user)
            location.href = './index.html'
          }
        }
        xhr.open('GET', url)
        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken)
        xhr.send()
      },
      handleReset (name) {
        this.$refs[name].resetFields()
      }
    }
  }
</script>
<style lang="scss" type="text/scss">
  .loginLogo1 {
    position: absolute;
    left: 50%;
    margin-left: -75px;
    top: 220px;
    z-index: 9;
  }
  .loginWrap{
    width: 100%;
    height: 100%;
    overflow: hidden;
    .tit{
      color: #fff;
      font-size: 50px;
      text-align: center;
      padding-top: 116px;
      z-index: 999;
      position: relative;
    }
  }
  .loginFrom {
    position: absolute;
    bottom: 60px;
    left: 50%;
    width: 260px;
    margin-left: -130px;
    z-index: 99;
    color: #fff;
    .loginBtn {
      width: 260px;
      margin-top: 12px;
      color: #333;
      background: #fff;
      border: #fff;
      opacity: 0.6;
      &:hover {
        opacity: 1;
        background: #83c326;
      }
    }
    .userName, .passWord {
      width: 100%;
      height: 50px;
      background: none;
      font-size: 15px;
      font-weight: bold;
      color: #fff;
      text-align: center;
      border: 0px none;
      border-bottom: 2px solid white;
      &:focus {
        outline: none;
        border: 0px none;
        border-bottom: 2px solid #83c326;
      }
    }
  }
</style>
