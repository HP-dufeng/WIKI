// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueResource from 'vue-resource'

import * as auth from './api/auth/auth.js'
import { app } from './api/app.js'
import store from './vuex/store.js'
import config from './config/config.js'
import {publishFn} from './config/publishFn.js'

import iView from 'iview'
import 'iview/dist/styles/iview.css'
import urlAppend from 'url-append'
import hxqh from 'hx-v'
import 'hx-v/dist/styles/common.css'

// import '../static/UE/ueditor.config.js'
import '../static/UE/ueditor.all.min.js'
import '../static/UE/lang/zh-cn/zh-cn.js'
import '../static/UE/ueditor.parse.min.js'

Vue.config.productionTip = false
Vue.use(VueResource)
Vue.use(iView)

// 存储项目url
let baseUrl = {
  identitySiteRoot: localStorage.getItem('identitySiteRootSite'),
  wiki: localStorage.getItem('wikiSite'),
  file: localStorage.getItem('fileSite')
}

Vue.use(hxqh, baseUrl)

// 开启开发工具
Vue.config.devtools = process.env.NODE_ENV !== 'production'
// 自定义变量
Vue.prototype.config = config
Vue.publicFn = publishFn
Vue.prototype.publishFn = publishFn
// 设置vue-resource认证
Vue.http.options.root = '/root'
Vue.http.headers.common['Authorization'] = 'Bearer ' + store.state.identity.user.access_token

Vue.http.options.before = function (request) {
  // Url增加一个随机数 处理IE缓存
  request.url = urlAppend(request.url, {r: Math.random()})
}

Vue.http.options.progress = function () {
  // 每个操作前检测是否超时
  if (!auth.loggedIn()) {
    location.href = 'login.html'
    return false
  }
}

router.beforeEach((to, from, next) => {
  if (!auth.loggedIn()) {
    Vue.prototype.$Modal.confirm({
      title: '登陆过期',
      content: '登陆已过期，请重新登陆',
      onOk: function () {
        location.href = 'login.html'  // 登录超时！返回首页，next 死循环
      }
    })
    return false
  } else {
    // 编辑保存提示
    let nowRouter = from.name
    let tip = to.query.tip
    if (tip === false) {
      next()
      return
    }
    if (config.routers.includes(nowRouter)) {
      Vue.prototype.$Modal.confirm({
        title: '保存提示',
        content: '文档还未保存或提交，确定离开本页面么？',
        onOk: function () {
          next()
        }
      })
    } else {
      next()
    }
  }
})

router.afterEach(route => {
  // 保存进历史
  app.addHistory(route)
})

/* eslint-disable*/
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: {
    App
  }
})

