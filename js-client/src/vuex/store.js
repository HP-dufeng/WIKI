/**
 * Created by wisdom on 2017/2/28.
 */
import Vue from 'vue'
import Vuex from 'vuex'

// 身份认证
import identity from './modules/auth/identity.js'
import common from './modules/page/page-common.js'
import dicBook from './modules/dictionaries/dictionaries.js'

// 公共actions方法
import * as actions from './actions.js'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  strict: debug,
  modules: {
    identity,   //  身份认证
    dicBook,   //  数据字典
    common    //  林琳碎碎 的一些data
  },
  actions
})
