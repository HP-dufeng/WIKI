<template>
  <div class="main" :class="{'layout-hide-text': menushort}" v-if="isLogin">
    <div class="layout-menu-left spanLeft" ref="menuLeft" id="menuLeft">
      <app-sidebar></app-sidebar>
    </div>
    <div class="spanRight">
      <app-header @toggleClick="toggleClick"></app-header>
      <app-content></app-content>
      <app-footer></app-footer>
    </div>
  </div>
</template>

<script>
  import $ from 'jquery'
  import Header from './page/layout/Header.vue'
  import Footer from './page/layout/Footer.vue'
  import SideBar from './page/layout/SideBar.vue'
  import Content from './page/layout/Content.vue'
  import {mapActions, mapGetters} from 'vuex'
  import {loggedIn} from './api/auth/auth'
  import * as authFunctin from './api/auth/auth_function.js'
  export default {
    name: 'app',
    data () {
      return {
        fullHeight: window.innerHeight,
        menushort: false
//                spanLeft: 4,
//                spanRight: 20
      }
    },
    components: {
      'app-header': Header,
      'app-content': Content,
      'app-footer': Footer,
      'app-sidebar': SideBar
    },
    computed: {
      ...mapGetters({
        user: 'user'
      }),
      isLogin: function () {
        return loggedIn()
      }
    },
    methods: {
      ...mapActions({
        updateUser: 'updateUser',
        siginInPopup: 'siginInPopup'
      }),
      signinSilent: function () {
        var _self = this
        authFunctin.signinAuth(_self.updateUser, _self.siginInPopup, null)
      },
      toggleClick () {
        this.menushort = !this.menushort
        let dom = $('#menuLeft')
        if (this.menushort === true) {
          dom.animate({
            width: '70'
          })
        } else {
          dom.animate({
            width: '230'
          })
        }
//        if (this.menushort === true) {
//          let width = 230
//          let _this = this
//          let start = setInterval(function () {
//            if (width !== 70) {
//              width = width - 5
//              _this.$refs.menuLeft.style.cssText += 'width:' + width + 'px !important'
//            } else {
//              clearInterval(start)
//            }
//          }, 10)
//        } else {
//          let width = 70
//          let _this = this
//          let start = setInterval(function () {
//            if (width !== 230) {
//              width = width + 5
//              _this.$refs.menuLeft.style.cssText += 'width:' + width + 'px !important'
//            } else {
//              clearInterval(start)
//            }
//          }, 10)
//        }
      }
    },
    mounted () {
      var _self = this
      // 登录检测函数
      authFunctin.Auth(_self.updateUser, _self.signinSilent, null)
//      let dom = $(this.$refs.menuLeft)
//      console.log(dom)
//      dom.hide()
//      $('.navicon-icon-button').hover(function () {
//        dom.show()
//      })
    }
  }
</script>
<style lang="css" type="text/css">
  .main {
    overflow: hidden;
    width: 100%;
    display: flex;
    background: #f5f7f9;
    position: relative;
  }

  .spanLeft {
    width: 230px;
  }

  .spanRight {
    overflow: hidden;
    flex: 1;
    min-width:1200px;
  }

  .layout {
    background: #f5f7f9;
    position: relative;
  }

  .layout-breadcrumb {
    padding: 10px 15px 0;
  }

  .layout-content {
    min-height: 200px;
    margin: 15px;
    overflow: hidden;
    background: #fff;
    border-radius: 4px;
  }

  .layout-content-main {
    padding: 10px;
  }

  .layout-copy {
    text-align: center;
    padding: 10px 0 20px;
    color: #9ea7b4;
  }

  .layout-menu-left {
    background: #464c5b;
  }

  .layout-header {
    height: 60px;
    background: #fff;
    box-shadow: 0 1px 1px rgba(0, 0, 0, .1);
  }

  .layout-logo-left {
    width: 90%;
    height: 30px;
    background: #5b6270;
    border-radius: 3px;
    margin: 15px auto;
  }

  .layout-ceiling-main a {
    color: #9ba7b5;
  }

  .layout-hide-text .layout-text {
    display: none;
  }

  .layout-hide-text .ivu-menu-submenu-title-icon {
    display: none;
  }

  .layout-hide-text .ivu-menu-vertical .ivu-menu-submenu .ivu-menu-item {
    padding-left: 24px;
  }

  .layout-hide-text .layout-menu-left .layout-logo-left {
    font-size: 14px;
    letter-spacing: 0;
  }

  .menu-one .ivu-menu-submenu-title-icon {
    display: none;
  }

  .ivu-menu-submenu-title a .layout-text {
    color: #9ea7b4;
  }

  .ivu-menu-submenu-title a i.ivu-icon-ios-navigate {
    margin-right: 8px;
    color: #9ea7b4;
  }

  .ivu-col {
    transition: width .2s ease-in-out;
  }

  i.ivu-icon-ios-navigate {
    font-size: 14px;
  }
</style>
